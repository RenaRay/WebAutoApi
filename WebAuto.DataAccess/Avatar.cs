namespace WebAuto.DataAccess
{
    public class Avatar
    {
        public int Id { get; set; }

        //1MB
        //varbinary(1048576)
        public byte[] Content { get; set; }

        //nvarchar(100)
        public string ContentType { get; set; }
    }
}
