namespace BusTimetable.Models
{
    // Class/Model for a passenger who can book tickets.
    public class Passenger
    {
        public int PassengerID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Passenger() { }

        public Passenger(int passengerId, string fullName, string email)
        {
            PassengerID = passengerId;
            FullName    = fullName;
            Email       = email;
        }

        public override string ToString() => $"[{PassengerID}] {FullName} <{Email}>";
    }
}
