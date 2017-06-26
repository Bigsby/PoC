using System;
using System.Collections.Generic;

namespace IQuerying.Implementations
{
    internal static class WebServiceHelper
    {
        private static int numResults = 200;
        private static bool mustHaveImage = false;

        internal static Place[] GetPlacesFromTerraServer(List<string> locations)
        {
            // Limit the total number of Web service calls. 
            if (locations.Count > 5)
                throw new InvalidQueryException("This query requires more than five separate calls to the Web service. Please decrease the number of locations in your query.");

            List<Place> allPlaces = new List<Place>();

            // For each location, call the Web service method to get data. 
            foreach (string location in locations)
            {
                Place[] places = CallGetPlaceListMethod(location);
                allPlaces.AddRange(places);
            }

            return allPlaces.ToArray();
        }

        private static Place[] CallGetPlaceListMethod(string location)
        {
            return new Place[] {
                new Place("Place One", "NY", PlaceType.CityTown),
                new Place("Place Two", "NY", PlaceType.HillMountain),
                new Place("Place Three", "WA", PlaceType.Island)
            };
        }
    }
}
