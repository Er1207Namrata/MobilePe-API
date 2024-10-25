using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusBlock
    {
        public string EndUserIp { get; set; }
        public string ResultIndex { get; set; }
        public string TraceId { get; set; }
        public string TokenId { get; set; }
        public int BoardingPointId { get; set; }
        public int DroppingPointId { get; set; }
        public List<Passenger> Passenger { get; set; }
    }

    public class GST
    {
        public decimal CGSTAmount { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal CessAmount { get; set; }
        public decimal CessRate { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal TaxableAmount { get; set; }
    }

    public class Passenger
    {
        public bool LeadPassenger { get; set; }
        public int PassengerId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Gender { get; set; }
        public object IdNumber { get; set; }
        public object IdType { get; set; }
        public string LastName { get; set; }
        public string Phoneno { get; set; }
        public Seat Seat { get; set; }
    }

    public class Price
    {
        public string CurrencyCode { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Tax { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal Discount { get; set; }
        public decimal PublishedPrice { get; set; }
        public decimal PublishedPriceRoundedOff { get; set; }
        public decimal OfferedPrice { get; set; }
        public decimal OfferedPriceRoundedOff { get; set; }
        public decimal AgentCommission { get; set; }
        public decimal AgentMarkUp { get; set; }
        public decimal TDS { get; set; }
        public GST GST { get; set; }
    }

   

    public class Seat
    {
        public string ColumnNo { get; set; }
        public int Height { get; set; }
        public bool IsLadiesSeat { get; set; }
        public bool IsMalesSeat { get; set; }
        public bool IsUpper { get; set; }
        public string RowNo { get; set; }
        public string SeatIndex { get; set; }
        public string SeatName { get; set; }
        public bool SeatStatus { get; set; }
        public int SeatType { get; set; }
        public int Width { get; set; }
        public Price Price { get; set; }
    }


    // Bus Block response

    public class BusBlockResponse
    {
        public BlockResult BlockResult { get; set; }
    }

    public class BlockResult
    {
        public bool IsPriceChanged { get; set; }
        public int ResponseStatus { get; set; }
        public BusError Error { get; set; }
        public string ArrivalTime { get; set; }
        public string BusType { get; set; }
        public string DepartureTime { get; set; }
        public string ServiceName { get; set; }
        public string TraceId { get; set; }
        public string TravelName { get; set; }
        public BoardingPointdetails BoardingPointdetails { get; set; }
        public List<CancelPolicy> CancelPolicy { get; set; }
        public DropingPointdetails DropingPointdetails { get; set; }
        public List<PassengerRes> Passenger { get; set; }
    }

    public class BoardingPointdetails
    {
        public string CityPointAddress { get; set; }
        public string CityPointContactNumber { get; set; }
        public int CityPointIndex { get; set; }
        public string CityPointLandmark { get; set; }
        public string CityPointLocation { get; set; }
        public string CityPointName { get; set; }
        public DateTime CityPointTime { get; set; }
    }

    public class CancelPolicy
    {
        public int CancellationCharge { get; set; }
        public int CancellationChargeType { get; set; }
        public string PolicyString { get; set; }
        public string TimeBeforeDept { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class DropingPointdetails
    {
        public int CityPointIndex { get; set; }
        public string CityPointLocation { get; set; }
        public string CityPointName { get; set; }
        public DateTime CityPointTime { get; set; }
    }

    public class GSTRes
    {
        public decimal CGSTAmount { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal CessAmount { get; set; }
        public decimal CessRate { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal TaxableAmount { get; set; }
    }

    public class PassengerRes
    {
        public bool LeadPassenger { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Gender { get; set; }
        public object IdNumber { get; set; }
        public object IdType { get; set; }
        public string LastName { get; set; }
        public string Phoneno { get; set; }
        public SeatRes Seat { get; set; }
    }

    public class Priceres
    {
        public string CurrencyCode { get; set; }
        public decimal BasePrice { get; set; }
        public int Tax { get; set; }
        public int OtherCharges { get; set; }
        public int Discount { get; set; }
        public decimal PublishedPrice { get; set; }
        public int PublishedPriceRoundedOff { get; set; }
        public decimal OfferedPrice { get; set; }
        public int OfferedPriceRoundedOff { get; set; }
        public decimal AgentCommission { get; set; }
        public int AgentMarkUp { get; set; }
        public decimal TDS { get; set; }
        public GSTRes GST { get; set; }
    }

    
    public class SeatRes
    {
        public string ColumnNo { get; set; }
        public int Height { get; set; }
        public bool IsLadiesSeat { get; set; }
        public string RowNo { get; set; }
        public double SeatFare { get; set; }
        public int SeatIndex { get; set; }
        public string SeatName { get; set; }
        public bool SeatStatus { get; set; }
        public int SeatType { get; set; }
        public int Width { get; set; }
        public Priceres Price { get; set; }
    }



}
