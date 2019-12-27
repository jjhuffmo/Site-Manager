using System.ComponentModel;

public class User_Info : INotifyPropertyChanged
{
    private int _User_ID;
    private bool _Modified;

    public string User_Name = "";
    public int Access = 0;

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    public int User_ID
    {
        get { return _User_ID; }
        set
        {
            _User_ID = value;
            OnPropertyChanged();
        }
    }

    public bool Modified
    {
        get { return _Modified; }
        set
        {
            _Modified = value;
            OnPropertyChanged();
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
