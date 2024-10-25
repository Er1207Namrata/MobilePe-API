using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model;
using MobileAPI_V2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IHostingEnvironment _env;
        private readonly IDataRepository _dataRepository;
        private readonly IConfiguration _configuration;
        public string razorpayupikey = string.Empty;
        public string razorpayupisecret = string.Empty;
        public string razorpaydebitcardkey = string.Empty;
        public string razorpaydebitcardsecret = string.Empty;
        public string razorpaycreditcardkey = string.Empty;
        public string razorpaycreditcardsecret = string.Empty;
        public string VenusRechargeURL = string.Empty;
        public string INSURANCEURLNEW = string.Empty;
        string razorpaycontactsUrl = string.Empty;
        string razorpayfundAccountUrl = string.Empty;
        string razorpaypayoutUrl = string.Empty;
        CommonJsonPostRequest CommonJsonPostRequestobj;
        ApiRequest request;
        JWTToken objJWT;
        SendSMS objsms;
        public ImageController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {
            _env = env;
            _dataRepository = dataRepository;
            _configuration = configuration;
            objJWT = new JWTToken(_dataRepository);
            objsms = new SendSMS(_configuration, _dataRepository);
            CommonJsonPostRequestobj = new CommonJsonPostRequest(_configuration);
            razorpayupikey = _configuration["razorpayupikey"];
            razorpayupisecret = _configuration["razorpayupisecret"];
            razorpaydebitcardkey = _configuration["razorpaydebitcardkey"];
            razorpaydebitcardsecret = _configuration["razorpaydebitcardsecret"];
            razorpaycreditcardkey = _configuration["razorpaycreditcardkey"];
            razorpaycreditcardsecret = _configuration["razorpaycreditcardsecret"];
            VenusRechargeURL = _configuration["VenusRecharge"];
            INSURANCEURLNEW = _configuration["INSURANCEURLNEW"];
            razorpaycontactsUrl = _configuration["razorpaycontactsUrl"];
            razorpayfundAccountUrl = _configuration["razorpayfundAccountUrl"];
            razorpaypayoutUrl = _configuration["razorpaypayoutUrl"];
            request = new ApiRequest(_configuration);
        }
        private readonly Dictionary<string, byte[]> _mimeTypes = new Dictionary<string, byte[]>
    {
        {"image/jpeg", new byte[] {255, 216, 255}},
        {"image/jpg", new byte[] {255, 216, 255}},
        //{"image/pjpeg", new byte[] {255, 216, 255}},
        //{"image/apng", new byte[] {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82}},
        {"image/png", new byte[] {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82}},
       // {"image/bmp", new byte[] {66, 77}},
        {"image/gif", new byte[] {71, 73, 70, 56}},
    };
        private bool ValidateMimeType(byte[] file, string contentType)
        {
            var imageType = _mimeTypes.SingleOrDefault(x => x.Key.Equals(contentType));

            return file.Take(imageType.Value.Length).SequenceEqual(imageType.Value);
        }

        [HttpPost("UploadFile")]
        public async Task<Common> UploadFile(long MemberId)
        {
            Common objcm = new Common();
            try
            {
                var f = HttpContext.Request.Form;
                var formkeys = HttpContext.Request.Form.Keys;
                string DocumentType = string.Empty;
                foreach (var key in formkeys)
                {
                    if (key != null && key.Length > 0)
                    {
                        DocumentType = key;
                    }
                }
                if (!string.IsNullOrEmpty(DocumentType))
                {
                    var files = HttpContext.Request.Form.Files;
                    var webRoot = _env.WebRootPath;
                    if (files.Count > 0)
                    {
                        foreach (var Image in files)
                        {
                            if (Image != null && Image.Length > 0)
                            {
                                var file = Image;
                                string childfolderpath = "/ReadWriteData/" + DocumentType;
                                var folderpath = webRoot + "\\ReadWriteData\\" + DocumentType;

                                var contenttype = file.ContentType;
                                int count = file.FileName.Split('.').Length - 1;
                                if (count == 1)
                                {
                                    if (contenttype == "image/png" || contenttype == "image/jpeg" || contenttype == "image/jpg" || contenttype == "image/pdf")
                                    {


                                        if (file.Length > 0 && file.Length < 2091009)
                                        {
                                            if (!Directory.Exists(folderpath))
                                            {
                                                Directory.CreateDirectory(folderpath);
                                            }
                                            string extension = Path.GetExtension(file.FileName);
                                            if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".pdf" || extension.ToLower() == ".png")
                                            {
                                                var fileName = file.FileName;
                                                var t = DateTime.Now.ToString("hhmmssfftt");
                                                //string savefilepath = childfolderpath + "/" + DocumentType+ "_" + t + "_" + MemberId.ToString() + extension;
                                                //var filepath = folderpath + "\\" + DocumentType+ "_" + t+ "_" + MemberId.ToString() + extension;

                                                string savefilepath = childfolderpath + "/" + DocumentType + "_" + MemberId.ToString() + "_" + t + extension;
                                                var filepath = folderpath + "\\" + DocumentType + "_" + MemberId.ToString()+"_"+ t + extension;

                                                if (System.IO.File.Exists(filepath))
                                                {
                                                    System.IO.File.Delete(filepath);
                                                }
                                                using (var fileStream = new FileStream(filepath, FileMode.Create))
                                                {
                                                    await file.CopyToAsync(fileStream);


                                                }
                                                var v = ValidateMimeType(System.IO.File.ReadAllBytes(filepath), contenttype);
                                                if (v)
                                                {
                                                    var res = await _dataRepository.ImageUpload(savefilepath, DocumentType, MemberId);
                                                    if (res != null && res.flag == 1)
                                                    {
                                                        objcm.transactionId= "https://apiv2.mobilepe.co.in" +savefilepath;
                                                        objcm.filepath = "https://apiv2.mobilepe.co.in" +savefilepath;
                                                        objcm.response = "success";
                                                        objcm.message = DocumentType + " uploded successfully";
                                                    }
                                                    else
                                                    {
                                                        objcm.response = "error";
                                                        objcm.message = "An error occured while uploading " + DocumentType;
                                                    }
                                                }
                                                else
                                                {
                                                    if (System.IO.File.Exists(filepath))
                                                    {
                                                        System.IO.File.Delete(filepath);
                                                    }
                                                    objcm.response = "error";
                                                    objcm.message = "Only jpeg,jpg,pdf,png files are allowed";
                                                }
                                            }
                                            else
                                            {
                                                objcm.response = "error";
                                                objcm.message = "Only jpeg,jpg,pdf,png files are allowed";
                                            }


                                        }
                                        else
                                        {
                                            objcm.response = "error";
                                            objcm.message = "Upto 2MB file size allowed";
                                        }
                                    }
                                    else
                                    {
                                        objcm.response = "error";
                                        objcm.message = "Invalid Content Type";
                                    }
                                }
                                else
                                {
                                    objcm.response = "error";
                                    objcm.message = "double extension not allowed";
                                }
                            }
                            else
                            {
                                objcm.response = "error";
                                objcm.message = "Please select file";
                            }
                        }
                    }
                    else
                    {
                        objcm.response = "error";
                        objcm.message = "Please select file";
                    }
                }

                else
                {
                    objcm.response = "error";
                    objcm.message = "document type missing";
                }

            }
            catch (Exception ex)
            {
                objcm.response = "error";
                objcm.message = ex.Message;
            }

            return objcm;
        }
        [HttpPost("UploadSupportFile")]
        public async Task<SupportResponse> UploadSupportFile(string MemberId)
        {
            SupportResponse objcm = new SupportResponse();
            try
            {
                string base64Decoded;
                byte[] database64 = System.Convert.FromBase64String(MemberId);
                base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(database64);
                long Id = Convert.ToInt64(base64Decoded);
                var formkeys = HttpContext.Request.Form.Keys;
                string DocumentType = string.Empty;
                foreach (var key in formkeys)
                {
                    if (key != null && key.Length > 0)
                    {
                        DocumentType = key;
                    }
                }
                if (!string.IsNullOrEmpty(DocumentType))
                {
                    var files = HttpContext.Request.Form.Files;
                    var webRoot = _env.WebRootPath;
                    if (files.Count > 0)
                    {
                        foreach (var Image in files)
                        {
                            if (Image != null && Image.Length > 0)
                            {
                                var file = Image;
                                string childfolderpath = "/ReadWriteData/" + DocumentType;
                                var folderpath = webRoot + "\\ReadWriteData\\" + DocumentType;

                                var contenttype = file.ContentType;
                                int count = file.FileName.Split('.').Length - 1;
                                if (count == 1)
                                {
                                    if (contenttype == "image/png" || contenttype == "image/jpeg" || contenttype == "image/jpg" || contenttype == "image/pdf")
                                    {


                                        if (file.Length > 0 && file.Length < 2091009)
                                        {
                                            if (!Directory.Exists(folderpath))
                                            {
                                                Directory.CreateDirectory(folderpath);
                                            }
                                            string extension = Path.GetExtension(file.FileName);
                                            if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".pdf" || extension.ToLower() == ".png")
                                            {
                                                var t = DateTime.Now.ToString("hhmmssfftt");
                                                var fileName = file.FileName;
                                                string savefilepath = childfolderpath + "/" + DocumentType + "_" + t +"_"+Id.ToString() + extension;
                                                var filepath = folderpath + "\\" + DocumentType + "_" + t+"_"+Id.ToString() + extension;

                                                if (System.IO.File.Exists(filepath))
                                                {
                                                    System.IO.File.Delete(filepath);
                                                }
                                                using (var fileStream = new FileStream(filepath, FileMode.Create))
                                                {
                                                    await file.CopyToAsync(fileStream);


                                                }
                                                var v = ValidateMimeType(System.IO.File.ReadAllBytes(filepath), contenttype);
                                                if (v)
                                                {
                                                    var res = await _dataRepository.ImageUpload(savefilepath, DocumentType, Id); ;
                                                    if (res != null && res.flag == 1)
                                                    {
                                                        objcm.response = "success";
                                                        if (DocumentType == "Support")
                                                        {
                                                            objcm.message = DocumentType + " file uploded successfully";
                                                        }
                                                        else
                                                        {
                                                            objcm.message = DocumentType + " uploded successfully";
                                                        }

                                                        objcm.imageurl = savefilepath;
                                                        objcm.SupportId = res.Id;

                                                    }
                                                    else
                                                    {
                                                        objcm.response = "error";
                                                        objcm.message = "An error occured while uploading " + DocumentType;
                                                    }
                                                }
                                                else
                                                {
                                                    if (System.IO.File.Exists(filepath))
                                                    {
                                                        System.IO.File.Delete(filepath);
                                                    }
                                                    objcm.response = "error";
                                                    objcm.message = "Only jpeg,jpg,pdf,png files are allowed";
                                                }
                                            }
                                            else
                                            {
                                                objcm.response = "error";
                                                objcm.message = "Only jpeg,jpg,pdf,png files are allowed";
                                            }


                                        }
                                        else
                                        {
                                            objcm.response = "error";
                                            objcm.message = "Upto 2MB file size allowed";
                                        }
                                    }
                                    else
                                    {
                                        objcm.response = "error";
                                        objcm.message = "Invalid Content Type";
                                    }
                                }
                                else
                                {
                                    objcm.response = "error";
                                    objcm.message = "double extension not allowed";
                                }
                            }
                            else
                            {
                                objcm.response = "error";
                                objcm.message = "Please select file";
                            }
                        }
                    }
                    else
                    {
                        objcm.response = "error";
                        objcm.message = "Please select file";
                    }
                }

                else
                {
                    objcm.response = "error";
                    objcm.message = "document type missing";
                }

            }
            catch (Exception ex)
            {
                objcm.response = "error";
                objcm.message = ex.Message;
            }

            return objcm;
        }
    }
}
