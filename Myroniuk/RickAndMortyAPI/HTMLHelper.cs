using RickAndMortyAPI.Entities;
using RickAndMortyAPI.Services;
using System.Reflection;
using System.Text;

namespace RickAndMortyAPI
{
    public class HTMLHelper
    {
        public static async Task<string> ToHTMLTable(ILocationService locationService, string? ids = null)
        {
            var sb = new StringBuilder("<h3>Locations:</h3><table>");
            PropertyInfo[] properties = typeof(Location).GetProperties();
            sb.Append("<tr>");
            foreach (PropertyInfo property in properties)
            {
                sb.Append($"<th>{property.Name}</th>");
            }
            sb.Append("</tr>");
            
            IAsyncEnumerable<Location> locations;
            if(ids != null) locations = locationService.GetLocationsAsync(ids);
            else locations = locationService.GetLocationsAsync();

            await foreach (var location in locations)
            {
                sb.Append("<tr>");
                foreach (PropertyInfo property in properties)
                {
                    sb.Append($"<td style='text-align:center'>{property.GetValue(location)}</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
