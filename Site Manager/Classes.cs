public class User_Info
{
    private int _User_ID;

    public string User_Name = "";
    public int User_ID
    {
        get { return _User_ID; }
        set
        {
            _User_ID = value;
            var Update = new Site_Manager.MainWindow();
            Update.Update_LoginStatus(_User_ID);
        }
    }
    public int Access = 0;
}
