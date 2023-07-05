namespace BowlingAlleyAPI.Services.Alleys
{
    public interface IAlleyService
    {
        Task<IResult> GetAllAlleys();

        Task<IResult> GetAvailableAlleys(DateTime startDate, DateTime endDate);
    }
}
