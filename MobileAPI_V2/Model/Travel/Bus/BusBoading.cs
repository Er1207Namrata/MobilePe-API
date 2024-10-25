using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusBoarding
    {
        public string EndUserIp { get; set; }
        public int ResultIndex { get; set; }
        public string TraceId { get; set; }
        public string TokenId { get; set; }
    }
    public class BusBoardingResponse
    {
        public BusRouteDetailResult GetBusRouteDetailResult { get; set; }
    }
    public class BusRouteDetailResult
    {
        public int ResponseStatus { get; set; }
        public BusError Error { get; set; }
        public string TraceId { get; set; }
        public List<BoardingPointsDetails> BoardingPointsDetails { get; set; }
        public List<DroppingPointsDetails> DroppingPointsDetails { get; set; }
    }
    public class BoardingPointsDetails
    {
        public string CityPointAddress { get; set; }
        public string CityPointContactNumber { get; set; }
        public long CityPointIndex { get; set; }
        public string CityPointLandmark { get; set; }
        public string CityPointLocation { get; set; }
        public string CityPointName { get; set; }
        public DateTime CityPointTime { get; set; }
    }
    public class DroppingPointsDetails
    {
        public long CityPointIndex { get; set; }
        public string CityPointLocation { get; set; }
        public string CityPointName { get; set; }
        public DateTime CityPointTime { get; set; }
    }

}
