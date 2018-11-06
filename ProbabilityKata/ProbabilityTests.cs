using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProbabilityKata
{
    public class ProbabilityTests
    {
        [Fact]
        public void UsingCtorReturnNewInstanceWithExpectedProperties()
        {
            var prob = new Probability(1);

            Assert.NotNull(prob);
            Assert.Equal(new Probability(1), prob);
        }

        [Fact]
        public void GivenProbability_WhenCallEither_ThenReturnNewProbability()
        {
            var prob = new Probability(1);
            var prob2 = new Probability(3);

            var newProb = prob.Either(prob2);

            Assert.Equal(new Probability(1), newProb);
        }

        [Fact]
        public void GivenProbability_WhenCallCombinedWith_ThenReturnNewProbability()
        {
            var prob = new Probability(1);
            var prob2 = new Probability(3);

            var newProb = prob.CombinedWith(prob2);

            Assert.Equal(new Probability(3), newProb);
        }

        [Fact]
        public void GivenProbability_WhenCallInverseOf_ThenReturnNewProbability()
        {
            var prob = new Probability(1);

            var newProb = prob.InverseOf();

            Assert.Equal(new Probability(0), newProb);
        }


    }

    public class Probability
    {
        private readonly decimal _value;


        public Probability(decimal value)
        {
            _value = value;
        }

        public Probability InverseOf()
        {
            // 1 - P(A)
            var a = this;
            return new Probability(new Probability(1) - a);
        }

        public Probability Either(Probability b)
        {
            //P(A) + P(B) - P(A)P(B)
            var a = this;
            return new Probability(a + b - a * b);
        }

        public Probability CombinedWith(Probability b)
        {
            // P(A)P(B)
            var a = this;
            return new Probability(a * b);
        }

        public static implicit operator decimal(Probability obj)
        {
            return obj._value;
        }

        protected bool Equals(Probability other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Probability) obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Probability left, Probability right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Probability left, Probability right)
        {
            return !Equals(left, right);
        }
    }
}
