namespace VeloceWebApp_7._0_.Models
{
    public class EditModel
    {
        public bool IsRecordBeingEdited = false;
        public EditModel() { }
        public static readonly EditModel instance = new EditModel();
    }
}
