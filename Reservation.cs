public record class Reservation
{
    public Room room;
    public string reserverName;
    public DateTime dateTime;
    public DateTime time;

    public Reservation(string roomid, string name, int day, int hour ){
        room=new Room();
        room.roomId=roomid;
        reserverName=name;
        dateTime = new DateTime(1, 1, day, 0, 00, 0);
        time = new DateTime(1, 1, 1, hour, 00, 0);

        //Console.WriteLine("\nRoom id: "+roomid+" Reserver Name: "+reserverName+" Day: "+ dateTime.Day+ " Hour: "+time.Hour);

    }

}