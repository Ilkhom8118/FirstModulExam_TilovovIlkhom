using FirstModeulExam.Models;

namespace FirstModeulExam.Services;

public class PhoneServices
{
    private List<Phone> phones;
    public PhoneServices()
    {
        phones = new List<Phone>();
    }

    public Phone Add(Phone added)
    {
        added.Id = Guid.NewGuid();
        phones.Add(added);
        return added;
    }
    public Phone GetById(Guid id)
    {
        foreach (var byId in phones)
        {
            if (byId.Id == id)
            {
                return byId;
            }
        }
        return null;
    }

    public bool Delete(Guid id)
    {
        var phone = GetById(id);
        if (phone is null)
        {
            return false;
        }
        phones.Remove(phone);
        return true;
    }
    public bool Update(Phone obj)
    {
        var id = GetById(obj.Id);
        if (id is null)
        {
            return false;
        }
        phones[phones.IndexOf(id)] = obj;
        return true;
    }
    public List<Phone> GetAll()
    {
        return phones;
    }
}
