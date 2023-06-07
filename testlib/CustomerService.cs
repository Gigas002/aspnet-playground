public class CustomerService
{
    private readonly ICustomerRepository _repository;
    
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }
    
    public string GetCustomerName(int id)
    {
        var customer = _repository.GetById(id);

        return customer.Name;
    }
}
