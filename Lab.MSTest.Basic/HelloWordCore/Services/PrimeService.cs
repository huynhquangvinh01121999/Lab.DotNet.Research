namespace HelloWordCore.Services
{
    public class PrimeService
    {
        public bool IsPrime(int candidate)
            => (candidate % 2) == 0 ? true : false;
    }
}
