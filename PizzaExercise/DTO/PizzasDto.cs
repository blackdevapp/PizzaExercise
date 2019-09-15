using System.Collections.Generic;

namespace PizzaExercise.DTO
{
    /// <summary>
    /// Get Pizzas Request and Response data transfer objects
    /// </summary>
    public class PizzasDto
    {
        /// <summary>
        /// Get Pizzas request data transfer object
        /// </summary>
        public class RequestDto
        {
            
        }

        /// <summary>
        /// Get Pizzas response data transfer object
        /// </summary>
        public class ResponseDto
        {
            /// <summary>
            /// List of topping combinations
            /// </summary>
            public List<ToppingCombination> toppingCombination { get; set; }
        }

        /// <summary>
        /// Topping combination
        /// </summary>
        public class ToppingCombination
        {
            /// <summary>
            /// List of topping names
            /// </summary>
            public List<string> toppings { get; set; }
        }
    }

    
}
