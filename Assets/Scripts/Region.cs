//Region Class used to store region names, latitudes and longitudes
public class Region 
{
    private string latitude;
    private string longitude;
    private string name;

    public string Latitude
    {
        get { return latitude; }
        set { latitude = value; }
    }

    public string Longitude
    {
        get{ return longitude; }
        set{ longitude = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Region(string latitude, string longitude, string name)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.name = name;
    } 
}
