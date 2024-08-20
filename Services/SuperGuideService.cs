using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class SuperGuideService
    {
        private GuideService guideService;
        private TourRealizationService tourRealizationService;
        private TourRatingService tourRatingService;
        private TourService tourService;
        public SuperGuideService(GuideService guideService,TourRealizationService tourRealizationService,TourRatingService tourRatingService,TourService tourService) 
        {
            this.guideService = guideService;
            this.tourRealizationService = tourRealizationService;
            this.tourRatingService = tourRatingService;
            this.tourService = tourService;
        }

        public void IsSuperGuide(int userId)
        {
            Dictionary<Language,int> languageCount = new Dictionary<Language,int>();
            foreach(var tourRealization in tourRealizationService.GetAllFinishedFromPastYearByUser(userId)) 
            {
                Language language = tourRealizationService.GetLanguageById(tourRealization.Id);
                if (languageCount.ContainsKey(language)) { languageCount[language]++; }
                else languageCount[language] = 1;
            }
            int maxCount = languageCount.Max(l => l.Value);
            if (maxCount >= 20) CheckAverageGradeOnTours(languageCount.First(kvp => kvp.Value == maxCount).Key, userId);
            else UpdateToGuide(userId);
        }

        private void CheckAverageGradeOnTours(Language mostFrequentlanguage, int userId)
        {
            double sumGrade = 0;
            int count = 0;
            foreach(var tourRealization in tourRealizationService.GetAllFinishedFromPastYearByUser(userId))
            {
                Language language=tourRealizationService.GetLanguageById(tourRealization.Id);
                if (mostFrequentlanguage.Id == language.Id)
                {
                    double averageGrade = tourRatingService.GetAverageGradeByTourRealizationId(tourRealization.Id);
                    if (averageGrade != 0)
                    {
                        sumGrade += averageGrade;
                        count++;
                    }
                }
            }
            if (count == 0) {  UpdateToGuide(userId); }
            else if (sumGrade / count > 4.0) { UpdateToSuperGuide(userId);}
            else UpdateToGuide(userId);
        }
        private void UpdateToSuperGuide(int userId)
        {
            Guide guide=guideService.GetByUserId(userId);
            if (!guide.IsSuperGuide)
            {
                guide.IsSuperGuide = true;
                guide.SuperGuideStartDate = DateOnly.FromDateTime(DateTime.Now.Date);
                guideService.Update(guide);
                tourService.UpdateTourPriority(guide.UserId,true);
            }
        }

        private void UpdateToGuide(int userId)
        {
            Guide guide = guideService.GetByUserId(userId);
            if (guide.IsSuperGuide)
            {
                guide.IsSuperGuide = false;
                guide.SuperGuideStartDate = DateOnly.FromDateTime(DateTime.MinValue.Date);
                guideService.Update(guide);
                tourService.UpdateTourPriority(guide.UserId,false);
            }
        }



    }
}
