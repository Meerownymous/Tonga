using System;
namespace Tonga
{
    public interface IMap<Key, Value>
    {
        Value this[Key key] { get; set; }
    }
}

