using SecondLevelLibrary;

namespace FirstLevelLibrary
{
    public static class FirstLevel
    {
        public static string GetContent()
        {
            return "First Level Content. " + SecondLevel.GetContent();
        }
    }
}
