using System.ComponentModel;

public class User_Info : INotifyPropertyChanged
{
    private int _User_ID;

    public string User_Name = "";
    public string Conn_Status = "Login...";
    public int Access = 0;

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
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
            if (_User_ID > 0) Conn_Status = "Logout";
            else Conn_Status = "Login...";
            OnPropertyChanged();
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
}

public class MyClass : INotifyPropertyChanged
{
    private string imageFullPath;

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    public string ImageFullPath
    {
        get { return imageFullPath; }
        set
        {
            if (value != imageFullPath)
            {
                imageFullPath = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}