using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel.Helpers;

namespace Application.Core.Helpers
{
    public sealed class Combo
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }

        public Combo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public static IEnumerable<Combo> CreateListByEnum<T>()
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
                throw new ArgumentException("T must be of type System.Enum");

            var dictionary = Enum.GetValues(enumType).Cast<T>().ToDictionary(k => k, v => (v as Enum).GetDescription());


            return dictionary.Select(e => new Combo(Convert.ToInt32(e.Key), e.Value)).ToList();
        }
    }
}
