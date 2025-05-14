namespace TerraIntel_lAI.Services
{
    public interface IAIService
    {
        /// <summary>
        /// Given a latitude and longitude, return an interesting fact string.
        /// </summary>
        Task<string> GetFactAsync(double latitude, double longitude);
    }
}