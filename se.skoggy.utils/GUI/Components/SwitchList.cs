using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.Input;
using se.skoggy.utils.UI;

namespace se.skoggy.utils.GUI.Components
{
    public class SwitchList : GuiComponent
    {
        public delegate void ItemSelected(object item);
        public event ItemSelected OnItemSelected;
        public event ItemSelected OnSelectedItemChanged;

        private int _selectedIndex;

        public SwitchList(GuiContext context) 
            : base(context)
        {
            Items = new ObservableCollection<object>();
            Items.CollectionChanged += (sender, args) => ValidateIndex();
            _selectedIndex = -1;
        }

        public ObservableCollection<object> Items { get; private set; }

        public override float Height
        {
            get { return Font.MeasureString("HEIGHT").Y *scale.Y; }
        }

        public override void OnFocused()
        {
            if (Items.Count > 0 && _selectedIndex == -1)
                _selectedIndex = 0;
            base.OnFocused();
        }

        public override void Update(float dt, IInputState input)
        {

            if (HasFocus)
            {
                if (input.ButtonClicked(Buttons.A))
                {
                    SelectCurrentItem();
                }
                else if (input.LeftClicked())
                {
                    PreviousItem();
                }
                else if (input.RightClicked())
                {
                    NextItem();
                }
            }

            base.Update(dt, input);
        }

        private void NextItem()
        {
            _selectedIndex++;
            if (_selectedIndex > Items.Count - 1)
                _selectedIndex = 0;
            ValidateIndex();

            if (OnSelectedItemChanged != null) OnSelectedItemChanged(Items[_selectedIndex]);
        }

        private void PreviousItem()
        {
            _selectedIndex--;
            if (_selectedIndex < 0)
                _selectedIndex = Items.Count - 1;
            ValidateIndex();

            if (OnSelectedItemChanged != null) OnSelectedItemChanged(Items[_selectedIndex]);
        }

        private void ValidateIndex()
        {
            if (Items.Count == 0)
                _selectedIndex = -1;
            else if (_selectedIndex == -1)
                _selectedIndex = 0;
        }

        private void SelectCurrentItem()
        {
            if (!HasCurrentItem)
                return;

            var selectedItem = Items[_selectedIndex];

            if (OnItemSelected != null)
            {
                OnItemSelected(selectedItem);
            }
        }

        private bool HasCurrentItem
        {
            get { return (_selectedIndex > -1 && _selectedIndex < Items.Count); }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!HasCurrentItem)
                return;

            var currentItem = Items[_selectedIndex];

            var t  = new Text(Text + currentItem.ToString(), TextAlign.Center)
            {
                position = position,
                rotation = rotation,
                scale = scale
            };
            t.color = HasFocus ? SelectedColor : color;
            t.Draw(spriteBatch, Font);
        }
    }
}
