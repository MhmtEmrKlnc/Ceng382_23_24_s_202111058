using System.Runtime.InteropServices;

public record class Reservation
{
    public Room room{get;set;}
    public string reserverName{get;set;}
    public DateTime dateTime{get;set;}
    public DateTime time{get;set;}

    public Reservation(Room _room, string name, int day, int hour ){
        room=_room;
        reserverName=name;
        dateTime = new DateTime(1, 1, day, 0, 00, 0);
        time = new DateTime(1, 1, 1, hour, 00, 0);

        //Console.WriteLine("\nRoom id: "+roomid+" Reserver Name: "+reserverName+" Day: "+ dateTime.Day+ " Hour: "+time.Hour);

    }
    public Reservation(){}

}