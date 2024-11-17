using System.Collections.Generic;
namespace Lms.Models.Abstract;

public abstract class Taggable {
    public HashSet<Lms.Models.Tag> Tags { get; set; } = new HashSet<Lms.Models.Tag>();
}