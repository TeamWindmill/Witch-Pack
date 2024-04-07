namespace Tools.Lerp
{
    public interface ILerpable
    {
        public void StartTransition();
        public void EndTransition();
        public float CurrentValue { get; set; }
        public float TargetValue { get; set; }
        
    }
}