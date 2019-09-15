using Newtonsoft.Json;
using PizzaExercise.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaExercise
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PizzasDto.ResponseDto pizzas = await GetAllToppings();
                if (pizzas == null)
                {
                    MessageBox.Show("Can not read pizzas Json.", "Error", MessageBoxButtons.OK);
                    return;
                }

                var sortedToppings = SortToppings(pizzas.toppingCombination);

                var topToppingCombinations = TopToppingCombinations(sortedToppings);

                var source = new BindingSource {DataSource = topToppingCombinations.Take(20)};
                dataGridView1.DataSource = source;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK);
            }
           
        }

        private async Task<PizzasDto.ResponseDto> GetAllToppings()
        {
        HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://files.olo.com/pizzas.json");
            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                contentString = "{ \"toppingCombination\": " + contentString + " }";
                return JsonConvert.DeserializeObject<PizzasDto.ResponseDto>(contentString);
            }
            else
            {
                return null;
            }
        }


        private List<PizzasDto.ToppingCombination> SortToppings(List<PizzasDto.ToppingCombination> toppingCombinations)
        {
            foreach (var toppingCombination in toppingCombinations)
            {
                toppingCombination.toppings.Sort();
            }

            return toppingCombinations;
        }
        private List<ToppingRank> TopToppingCombinations(List<PizzasDto.ToppingCombination> toppingCombinations)
        {
            int rank = 1;
            var topToppingCombinations = toppingCombinations.GroupBy(i => String.Join(",",i.toppings))
                .Select(grp => new ToppingRank()
                {
                    Toppings = grp.Key,
                    OrderCount = grp.Count()
                }).OrderByDescending(o=>o.OrderCount).Select(s=> new ToppingRank()
                {
                    OrderCount = s.OrderCount,
                    Rank = rank++,
                    Toppings = s.Toppings
                })
                .ToList();

            return topToppingCombinations;
        }

        private class ToppingRank
        {
            public int Rank { get; set; }
            public string Toppings { get; set; }
            public int OrderCount { get; set; }
        }
    }
}
