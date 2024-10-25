using System.Collections.Generic;
using System.Data;

namespace MobileAPI_V2.Model.BBPS
{
    public class BillPay
    {
        public int Fk_MemId { get; set; }
        public string biller_code { get; set; }
        public string OrderId { get; set; }
        public string agent_code { get; set; }
        public int amount { get; set; }
        public string remakrs { get; set; }
        public string ref_id { get; set; }
        public string mobile { get; set; }
        public string client_ref { get; set; }
        public string quick_pay { get; set; }
        public List<CustomerParam> customer_params { get; set; }
        public PlanDetails plan_details { get; set; }
    }
    public class CustomerParam
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class PlanDetails
    {
        public string id { get; set; }
        public string type { get; set; }
    }
     public class BillPayRequest_log
    {
        public string bill_code { get; set; }
        public string agent_code { get; set; }
        public int amount { get; set; }
        public string remakrs { get; set; }
        public string ref_id { get; set; }
        public string mobile { get; set; }
        public string client_ref { get; set; }
        public string quick_pay { get; set; }
        public string plan_details_id { get; set; }
        public string plan_details_type { get; set; }
        public string Type { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public List<BillPayCustomerResponse_log> customerlist { get; set; }
    }
    public class BillPayCustomerResponse_log
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class BBPSCommonResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public object result { get; set; }

    }
    public class BillValidate
    {
        public string biller_code { get; set; }
        public string agent_code { get; set; }
        public string mobile { get; set; }
        public List<CustomerParam> customer_params { get; set; }


    }
    public class CustomerParamPrePaid
    {
        public string name { get; set; }
        public string value { get; set; }
        public List<CustomerParam> customer_params { get; set; }
    }
    public class BillValidatePrePaid
    {
        public string biller_code { get; set; }
        public string agent_code { get; set; }
        public string mobile { get; set; }
        public List<CustomerParamPrePaid> customer_params { get; set; }
    }

    public class customer_params
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class BillFetch
    {
        public string biller_code { get; set; }
        public string agent_code { get; set; }
        public string mobile { get; set; }
        public List<CustomerParam> customer_params { get; set; }

    }

    public class BBPSTranction
    {
        public string client_ref { get; set; }
    }
    public class TranctionStatusResponse
    {
        public string status { get; set; }
        public string reason { get; set; }
        public List<TxnResponse> response { get; set; }
        public List<TxnBillerResponse> biller_response { get; set; }

    }
    public class TxnResponse
    {
        public string status { get; set; }
        public string reason { get; set; }
        public string ref_id { get; set; }
        public string approval_ref_num { get; set; }
        public string txn_reference_id { get; set; }
        public List<CustomerParam> customer_params { get; set; }
    }
    public class TxnBillerResponse
    {
        public string customer_name { get; set; }
        public string amount { get; set; }
        public string due_date { get; set; }
        public string cust_conv_fee { get; set; }
        public string cust_conv_desc { get; set; }
        public string bill_date { get; set; }
        public string bill_number { get; set; }
        public string bill_period { get; set; }
        public string bill_tags { get; set; }
        public string additionalInfo { get; set; }

    }
}


