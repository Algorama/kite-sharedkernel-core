namespace SharedKernel.Domain.Entities
{
    public class User : EntityBase, IAggregateRoot
    {               
        public string Name     { get; set; }
        public string Login    { get; set; }
        public string Password { get; set; }
    }
}