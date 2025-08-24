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

            fTimer = new System.Windows.Forms.Timer { Interval = 15000 }; // tick κάθε 1s
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
 

        // ● events
        public event EventHandler<DateTime> Saved;
    }
}
