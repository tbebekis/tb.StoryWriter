namespace StoryWriter
{
    /// <summary>
    /// Minimal, safe autosave: σώζει μόνο όταν ο χρήστης είναι ανενεργός
    /// (IdleThreshold) και έχει περάσει διάστημα από το τελευταίο save (Interval).
    /// </summary>
    public class AutoSaveService : IDisposable
    {
        // ● private 
        static System.Threading.Lock syncLock = new();

        readonly System.Windows.Forms.Timer fTimer;
        readonly Action SaveProc;

        bool IsDirty;
        bool IsSaving;

        void Execute()
        {
            lock (syncLock)
            {
                if (!Enabled || !IsDirty || IsSaving) return;

                IsSaving = true;
                try
                {
                    SaveProc();
                    IsDirty = false;
                    Saved?.Invoke(this, DateTime.Now);
                }
                catch
                {
                    // nothing
                }
                finally
                {
                    IsSaving = false;
                }
            }

        }
 
 
        // ● public 
        public AutoSaveService(Action SaveProc)
        {
            this.SaveProc = SaveProc;

            fTimer = new System.Windows.Forms.Timer();
            AutoSaveSecondsInterval = 15;
            fTimer.Tick += (t, e) => Execute(); 
        }
        public void MarkAsDirty()
        {
            lock (syncLock)
            {
                IsDirty = true;
            }           
        }
 
        public void Dispose() => fTimer?.Dispose();

        // ● properties
        public bool Enabled
        {
            get => fTimer.Enabled;
            set => fTimer.Enabled = value;
        }
        public int AutoSaveSecondsInterval
        {
            get => fTimer.Interval / 1000;
            set => fTimer.Interval = value * 1000;
        }

        // ● events
        public event EventHandler<DateTime> Saved;
    }
}
