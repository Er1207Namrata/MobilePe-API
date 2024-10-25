using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;


namespace MobileAPI_V2.Model
{
    public class AutoMapperConfiguration: Profile
    {

       
        public   AutoMapperConfiguration()
        {
            //CreateMap<WalletCreationRequestV2DTO,WalletCreationRequestV2 >()
            //      .ForMember(t => t.memberId, i => i.Ignore());

            //CreateMap< WalletCreationRequestV2,WalletCreationRequestV2DTO > ();

            CreateMap<WalletCreationRequestV2, WalletCreationRequestV2DTO>()
                 .ForMember(t => t.memberId, i => i.Ignore());

            CreateMap<WalletCreationRequestV2DTO, WalletCreationRequestV2>();
        }

}
}
