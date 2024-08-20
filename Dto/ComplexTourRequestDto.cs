using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class ComplexTourRequestDto:INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int TouristId { get; set; }

        private string state;

        public string State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    OnPropertyChanged("State");
                }
            }
        }

        public ComplexTourRequestDto()
        {
        }
        public ComplexTourRequestDto(ComplexTourRequest complexTourRequest)
        {
            Id = complexTourRequest.Id;
            TouristId = complexTourRequest.TouristId;
            if (complexTourRequest.State.ToString().Equals("Pending"))
            {
                State = "Pending";
            }
            else if (complexTourRequest.State.ToString().Equals("Accepted"))
            {
                State = "Accepted";
            }
            else
            {
                State = "Expired";
            }
        }
        public ComplexTourRequest ToComplexTourRequest()
        {
            STATE requestState;
            if (State.Equals("Pending"))
            {
                requestState = STATE.Pending;
            }
            else if (State.Equals("Accepted"))
            {
                requestState = STATE.Accepted;
            }
            else
            {
                requestState = STATE.Expired;
            }

            return new ComplexTourRequest(Id,TouristId,requestState);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
    }
}
