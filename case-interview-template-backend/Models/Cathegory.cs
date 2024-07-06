namespace case_interview_template_backend.Models;

public class Cathegory
{
    public int id { get; set; }
    public string name { get; set; }
    public List<Product> products { get; set; }
}