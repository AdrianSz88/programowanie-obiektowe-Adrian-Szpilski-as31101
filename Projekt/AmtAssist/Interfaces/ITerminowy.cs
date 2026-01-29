namespace AmtAssist.Interfaces
{
    public interface ITerminowy
    {
        DateTime? PobierzTermin();
        bool CzyPilne();
        int DniDoTerminu();
    }
}