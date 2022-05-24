namespace ToDoApp.Tests
{
    public class ToDoTests
    {
        [Fact]
        public void TodaysDate_WhenAppStarts_ThenReturnsToDaysDate()
        {
            string response = ConfigurationModel.TodaysDate();

            Assert.Equal(DateTime.Now.ToString("MMMM dd"), response);
        }
    }
}