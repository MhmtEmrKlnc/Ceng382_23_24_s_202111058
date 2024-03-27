public class ReservationHandler
{
    private Reservation[][] reservations;
    private int gldays, glhours;
    public ReservationHandler(int days, int hours){
        gldays=days+1;
        glhours=hours;
        reservations = new Reservation[days+1][];
        for(int i=0;i<days+1;i++){
            reservations[i]=new Reservation[hours];
        }
    }
    public void addReservation(Reservation reservation){
        int day = reservation.dateTime.Day;
        int hour = reservation.time.Hour;
        if(reservations[day][hour]!=null){
            Console.WriteLine("Bu gün ve saatte başka bir randevu var, lütfen başka bir gün-saat seçiniz\n");
        }
        else{
            reservations[day][hour] = reservation;
            Console.WriteLine(reservation.room.roomId+" id'li("+reservation.room.roomName+" isimli) oda "+reservations[day][hour].reserverName+" isimli kişiye "+reservations[day][hour].dateTime.Day+".gün, saat "+reservations[day][hour].time.Hour+":00 için rezerve edilmiştir.\n");
        }
        
    }
    public void deleteReservation(Reservation reservation){
        int day = reservation.dateTime.Day;
        int hour = reservation.time.Hour;

        Reservation[][] updatedReservations = new Reservation[gldays+1][];
        for(int i=0;i<gldays+1;i++){
            updatedReservations[i]=new Reservation[glhours];
        }
        for (int i = 0; i < gldays; i++)
        {
            for (int j = 0; j < glhours; j++)
            {
                if (i == day && j == hour)
                {
                    continue;
                }
                updatedReservations[i][j] = reservations[i][j];
            }
        }
        reservations = updatedReservations;

        //Console.WriteLine("\nReservation silindi\n");
        
    }
    public void displayWeeklySchedule(){

        Console.WriteLine("\nTablodaki satırlar günleri, sütunlar saatleri göstermektedir. Tablo o gün ve saate denk gelen, rezervasyonu olan odanın id'sini göstermektedir.");
        Console.Write("  |");
        for (int j = 0; j < glhours; j++)
        {
            Console.Write(j.ToString("D2") + ":00|");
        }
        Console.Write("\n");
        for (int i = 1; i < gldays; i++)
        {
            Console.Write(i + " | ");
            for (int j = 0; j < glhours; j++)
            {
                
                if(reservations[i][j]==null){
                    Console.Write("--- | ");
                }
                else{
                    Console.Write(reservations[i][j].room.roomId+" | ");
                }
                
            }
        Console.Write("\n");
        }
    }


}