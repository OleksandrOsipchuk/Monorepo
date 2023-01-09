using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;

public class StudentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StudentLevel StudentLevel { get; set; }
    public string? GithubLink { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsRegistered { get; set; }
    public TelegramUser? TelegramUser { get; set; }
    public Subscription? Subscription { get; set; }

    public StudentDTO TransformToDTO(Student student)
    {
        return new StudentDTO
        {
            Id = student.Id,
            Name = student.Name,
            StudentLevel = student.StudentLevel,
            GithubLink = student.GithubLink,
            IsAdmin = student.IsAdmin,
            IsRegistered = student.IsRegistered,
            TelegramUser = student.TelegramUser,
            Subscription = student.Subscription
        };
    }

    public IEnumerable<StudentDTO> TransformToDTOList(IEnumerable<Student> students)
    {
        var StudentsDTOList = new List<StudentDTO>();
        foreach (Student s in students)
        {
            StudentsDTOList.Add(new StudentDTO()
            {
                Id = s.Id,
                Name = s.Name,
                StudentLevel = s.StudentLevel,
                GithubLink = s.GithubLink,
                IsAdmin = s.IsAdmin,
                IsRegistered = s.IsRegistered,
                TelegramUser = s.TelegramUser,
                Subscription = s.Subscription
            });
        }
        return StudentsDTOList;
    }
}
