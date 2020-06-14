using System.Collections.Generic;
using System.Linq;
using WebLab.DAL.Entities;

namespace WebLab.Models
{
	public class Cart {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart() {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary> 
        /// Количество объектов в корзине 
        /// </summary> 
        public int Count {
            get {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Количество убойной силы
        /// </summary>
        public int Forces {
            get {
                return Items.Sum(item => item.Value.Quantity * item.Value.Military.Force);
            }
        }

        /// <summary> 
        /// Добавление в корзину 
        /// </summary> 
        /// <param name="military">добавляемый объект</param> 
        public virtual void AddToCart(Military military) {
            // если объект есть в корзине 
            // то увеличить количество 
            if (Items.ContainsKey(military.MilitaryId))
                Items[military.MilitaryId].Quantity++;
            // иначе - добавить объект в корзину 
            else
                Items.Add(military.MilitaryId, new CartItem {
                    Military = military, Quantity = 1
                });
        }

        /// <summary> 
        /// Удалить объект из корзины 
        /// </summary> 
        /// <param name="id">id удаляемого объекта</param> 
        public virtual void RemoveFromCart(int id) {
            Items.Remove(id);
        }

        /// <summary> 
        /// Очистить корзину 
        /// </summary> 
        public virtual void ClearAll() {
            Items.Clear();
        }
    }

    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem {
        public Military Military { get; set; }
        public int Quantity { get; set; }
    }
}
