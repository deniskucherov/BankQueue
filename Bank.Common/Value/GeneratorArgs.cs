using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Value
{
    public sealed class GeneratorArgs
    {
        /// <summary>
        /// Arguments for new customers generator
        /// </summary>
        /// <param name="generationPeriod">min value is 100ms</param>
        /// <param name="maxGeneratedCount"></param>
        public GeneratorArgs(int generationPeriod, int maxGeneratedCount)
        {
            if (generationPeriod < 100)
                throw new ApplicationException("generationPeriod < 100");

            GenerationPeriod = generationPeriod;
            MaxGeneratedCount = maxGeneratedCount;
        }

        public int GenerationPeriod { get; private set; }
        public int MaxGeneratedCount { get; private set; }
    }
}
