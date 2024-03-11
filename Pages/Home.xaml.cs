using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMate.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.DTOs;
using MosqueMateServices.Enums;
using MosqueMateServices.Helper;
using MosqueMateServices.Helper.API;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using Newtonsoft.Json;
using PrayIDataServices.Helper.API;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MosqueMate.Pages
{
    public partial class home : Page
    {
        INotificationWindows notification;
        IAppData appData;
        IZekr zekr;
        private IAudioPlayer audioPlayer;
        private int zekrId = 1;
        private int ZekrIndex = 1;
        private int ZekrIndexCount = 1;
        PrayerTimesEnum prayerNowEnum;
        public bool isPanelShow { get; private set; }
        private bool showPrayerTimeNow { get; set; } = true;
        private readonly API aPI;
        private List<DTOAzkar> zekrTime;
        public home()
        {
            InitializeComponent();
            notification = new NotificationHelper(Settings.Default.notification);
            appData = AppDataRepo.Instance;
            API.url = "https://api.aladhan.com/v1/timingsByCity/{DateTime.Now:dd-MM-yyyy}?" + $"city={appData.City}&country={appData.Country}&method={appData.method}";
            this.zekr = new ZekrRepository();
            AdhanStatus();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region GetZekr(Morning-night)
            DateTime currentTime = DateTime.Now;
            DateTime ninePMToday = DateTime.Today.Add(new TimeSpan(21, 0, 0));
            var result = currentTime > ninePMToday ? "المساء" : "الصباح";
            zekrTime = zekr.GetZekrByName(result);
            #endregion
            LoadPage();
        }
        private void LoadPage()
        {
            PrayerTimeLeftAsync();
            BackgroundTask.ExecuteThreadUI(async () =>
            {
                using (ResourceJsonRepo resource = new ResourceJsonRepo())
                {
                    #region Prayer_Resources.json
                    prayersMenuLBL.Text = resource["PrayerTime"];
                    fajrItem.Title = resource[$"prayerTimes.{PrayerTimesEnum.Fajr}"];
                    sunraiseItem.Title = resource[$"prayerTimes.{PrayerTimesEnum.Sunrise}"];
                    duhrItem.Title = resource[$"prayerTimes.{PrayerTimesEnum.Dhuhr}"];
                    asrItem.Title = resource[$"prayerTimes.{PrayerTimesEnum.Asr}"];
                    magribItem.Title = resource[$"prayerTimes.{PrayerTimesEnum.Maghrib}"];
                    ishaItem.Title = resource[$"prayerTimes.{PrayerTimesEnum.Isha}"];
                    #endregion

                    if (appData.API_DATA == null)
                    {
                        #region API_Data
                        var responce = await API.GetMethodAsync();
                        if (responce != null)
                        {

                            #region SendRequestAPI
                            var desrialize = JsonConvert.DeserializeObject<PrayerTimesResponse>(responce);
                            appData.API_DATA = desrialize;
                            double latitude = desrialize.Data.Meta.Latitude;
                            double longtuide = desrialize.Data.Meta.Longitude;
                            API.url = $"https://api.aladhan.com/v1/qibla/{latitude}/{longtuide}";
                            var direction = JsonConvert.DeserializeObject<QiblaAPI>(await API.GetMethodAsync());

                            #endregion

                            #region SetPrayerTimes
                            fajrItem.Desc = desrialize.Data.Timings.Fajr.ToString("hh:mm tt");
                            sunraiseItem.Desc = desrialize.Data.Timings.Sunrise.ToString("hh:mm tt");
                            duhrItem.Desc = desrialize.Data.Timings.Dhuhr.ToString("hh:mm tt");
                            asrItem.Desc = desrialize.Data.Timings.Asr.ToString("hh:mm tt");
                            magribItem.Desc = desrialize.Data.Timings.Maghrib.ToString("hh:mm tt");

                            ishaItem.Desc = desrialize.Data.Timings.Isha.ToString("hh:mm tt");
                            #endregion

                            #region CastAPI_Data
                            var nextPrayerEnum = DateTimeHelper.GetNextPrayer(desrialize.Data.Timings);
                            DateTimeHelper.georgianDate = desrialize.Data.Date.Gregorian.Date;
                            DateTimeHelper.hijriDate = Settings.Default.currentLang == "ar" ?
                            desrialize.Data.Date.Hijri.Day + " - " +
                            desrialize.Data.Date.Hijri.Month.Arabic + "-" +
                            desrialize.Data.Date.Hijri.Year
                            :
                            desrialize.Data.Date.Hijri.Day + "-" +
                            desrialize.Data.Date.Hijri.Month.English + "-" + desrialize.Data.Date.Hijri.Year;
                            prayerNowEnum = nextPrayerEnum;

                            #endregion


                        }
                        else
                        {
                            notification.ShowNotification("Error", resource["InternetTimeout"], System.Windows.Forms.ToolTipIcon.Error);
                        }
                        #endregion
                    }
                    else
                    {
                        #region SetPrayerTimes
                        fajrItem.Desc = appData.API_DATA.Data.Timings.Fajr.ToString("hh:mm tt");
                        sunraiseItem.Desc = appData.API_DATA.Data.Timings.Sunrise.ToString("hh:mm tt");
                        duhrItem.Desc = appData.API_DATA.Data.Timings.Dhuhr.ToString("hh:mm tt");
                        asrItem.Desc = appData.API_DATA.Data.Timings.Asr.ToString("hh:mm tt");
                        magribItem.Desc = appData.API_DATA.Data.Timings.Maghrib.ToString("hh:mm tt");

                        ishaItem.Desc = appData.API_DATA.Data.Timings.Isha.ToString("hh:mm tt");
                        #endregion

                        #region CastAPI_Data
                        DateTimeHelper.georgianDate = appData.API_DATA.Data.Date.Gregorian.Date;
                        DateTimeHelper.hijriDate = Settings.Default.currentLang == "ar" ?
                        appData.API_DATA.Data.Date.Hijri.Year + " - (" +
                        appData.API_DATA.Data.Date.Hijri.Month.Arabic + ")" +
                        appData.API_DATA.Data.Date.Hijri.Day :
                        appData.API_DATA.Data.Date.Hijri.Day + "-" +
                        appData.API_DATA.Data.Date.Hijri.Month.English + "-" + appData.API_DATA.Data.Date.Hijri.Year;
                        var nextPrayerEnum = DateTimeHelper.GetNextPrayer(appData.API_DATA.Data.Timings);
                        prayerNowEnum = nextPrayerEnum;
                        timeLeftCard.Title = DateTimeHelper.GetSubPrayers() == null ? resource["TimeLeft"] + " : " : resource["TimeLeft"];
                        timeLeftCard.Number = DateTimeHelper.GetSubPrayers();
                        #endregion
                    }
                }
            });

        }
        private void PrayerTimeLeftAsync()
        {
            BackgroundTask.RunTaskThreadUIWithWhile(1000, () =>
            {
                using ResourceJsonRepo resource = new ResourceJsonRepo();
                timeLeftCard.Title = DateTimeHelper.GetSubPrayers() == null ? resource["TimeLeft"] + " : " : resource["TimeLeft"];
                timeLeftCard.Number = DateTimeHelper.GetSubPrayers();
                ChangeColor(prayerNowEnum);


            });

        }

        private void ChangeColor(PrayerTimesEnum prayer)
        {
            switch (prayer)
            {
                case PrayerTimesEnum.Fajr:
                    CustomControl.ChangeBackForeColors<Control>(fajrItem, "#FFFFFF", "#000000");
                    CustomControl.ChangeBackForeColors<Control>(sunraiseItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(duhrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(asrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(magribItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(ishaItem, "#31407B", "#BCCAF1");
                    break;
                case PrayerTimesEnum.Sunrise:
                    CustomControl.ChangeBackForeColors<Control>(sunraiseItem, "#FFFFFF", "#000000");
                    CustomControl.ChangeBackForeColors<Control>(fajrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(duhrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(asrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(magribItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(ishaItem, "#31407B", "#BCCAF1");
                    break;
                case PrayerTimesEnum.Dhuhr:
                    CustomControl.ChangeBackForeColors<Control>(duhrItem, "#FFFFFF", "#000000");
                    CustomControl.ChangeBackForeColors<Control>(fajrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(sunraiseItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(asrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(magribItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(ishaItem, "#31407B", "#BCCAF1");
                    break;
                case PrayerTimesEnum.Asr:
                    CustomControl.ChangeBackForeColors<Control>(asrItem, "#FFFFFF", "#000000");
                    CustomControl.ChangeBackForeColors<Control>(fajrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(sunraiseItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(duhrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(magribItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(ishaItem, "#31407B", "#BCCAF1");
                    break;
                case PrayerTimesEnum.Maghrib:
                    CustomControl.ChangeBackForeColors<Control>(magribItem, "#FFFFFF", "#000000");
                    CustomControl.ChangeBackForeColors<Control>(fajrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(sunraiseItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(duhrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(asrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(ishaItem, "#31407B", "#BCCAF1");
                    break;
                case PrayerTimesEnum.Isha:
                    CustomControl.ChangeBackForeColors<Control>(ishaItem, "#FFFFFF", "#000000");
                    CustomControl.ChangeBackForeColors<Control>(fajrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(sunraiseItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(duhrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(asrItem, "#31407B", "#BCCAF1");
                    CustomControl.ChangeBackForeColors<Control>(magribItem, "#31407B", "#BCCAF1");
                    break;
                default:
                    break;
            }
        }

        private void randomZekrCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isPanelShow)
            {
                #region setZekrCardValues
                randomZekrCard.ToolTip = zekrTime[0].category;
                randomZekrCard.Title = Settings.Default.currentLang == "ar" ? $" الذكر رقم ({zekrId})  " : $"  ({zekrId}) : Zekr Index";
                randomZekrCard.Number = Settings.Default.currentLang == "ar" ? "عدد الأذكار : " + zekrTime.Count : $"({zekrTime.Count}):Zekr Count";
                #endregion
                ShowPanel();
                zekrCounter.Focus();
                if (zekrTime != null)
                {
                    ZekrIndex = 1; // reset index when toggle panel 
                    var zekrContent = zekrTime[ZekrIndex - 1];
                    zekrTitle.Content = zekrContent.category;
                    zekrText.Text = zekrContent.zekr;
                    zekrCounter.Content = $"   {zekrContent.count}";
                    ZekrIndexCount = Convert.ToInt32(zekrContent.count);
                    SetLabelsZekr();

                }
                isPanelShow = true;
            }
            else
            {
                HidePanel();
                isPanelShow = false;

            }
        }

        private void ShowPanel()
        {
            using ResourceJsonRepo resource = new ResourceJsonRepo();
            var r = resource["PrayerTime"];

            zeker_container.Visibility = Visibility.Visible;
            Storyboard slideDownStoryboard = (Storyboard)FindResource("SlideDownStoryboard");
            slideDownStoryboard.Begin();
            isPanelShow = true;
        }
        private void HidePanel()
        {
            zeker_container.Visibility = Visibility.Visible;
            Storyboard slideDownStoryboard = (Storyboard)FindResource("SlideUpStoryboard");
            slideDownStoryboard.Begin();
            isPanelShow = false;
            ZekrIndex = 1;
        }
        private void AdhanStatus()
        {
            BackgroundTask.RunTaskWithWhile(1000, () =>
            {
                using ResourceJsonRepo resource = new ResourceJsonRepo();
                if (DateTimeHelper.IsPrayerTimeNow())
                {
                    using (ResourceHelper helper = new ResourceHelper("MosqueMateMedia.Properties.AudioResources"))
                    {
                        if (showPrayerTimeNow)
                        {
                            var muzzinAudio = helper.GetWavResxByName(Settings.Default.MuezzinFilename);
                            audioPlayer = new SoundHelper(muzzinAudio);
                            audioPlayer.PlayAudio();
                            notification.ShowNotification(resource["Adhan"], resource["prayerTimes.TimeForPrayer"] + resource[$"prayerTimes.{prayerNowEnum}"], System.Windows.Forms.ToolTipIcon.Info);
                            showPrayerTimeNow = false;
                        }
                    }
                }
                if (DateTimeHelper.IsLessHalfHour())
                {
                    notification.ShowNotification("Alert", resource["AlertNotify"], System.Windows.Forms.ToolTipIcon.Warning);
                }
            });

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void zeker_container_KeyDown(object sender, KeyEventArgs e)
        {
            if (zekrId != 0)
            {
                if (e.Key == Key.Left)
                {
                    e.Handled = true;
                    prevZekr();
                    SetLabelsZekr();
                }
                else if (e.Key == Key.Right)
                {
                    e.Handled = true;

                    nextZekr();
                    SetLabelsZekr();
                }
            }

        }
        private void nextZekr()
        {
            if (ZekrIndex == zekrTime.Count)
                return;
            ZekrIndex++;
            var zekr = this.zekr.ZekrPagination(zekrTime, ZekrIndex, 1);
            zekrText.Text = zekr.zekr;
            zekrCounter.Content = $"   {zekr.count}";
            ZekrIndexCount = Convert.ToInt32(zekr.count);
        }
        private void prevZekr()
        {
            if (ZekrIndex == 1)
                return;
            ZekrIndex--;
            var zekr = this.zekr.ZekrPagination(zekrTime, ZekrIndex, 1);
            zekrText.Text = zekr.zekr;
            zekrCounter.Content = $"   {zekr.count}";
            ZekrIndexCount = Convert.ToInt32(zekr.count);
        }
        private void SetLabelsZekr()
        {
            using (ResourceJsonRepo resource = new ResourceJsonRepo())
            {

                #region SetZekrLabels
                var timeInFileIndex = zekrTime.Count > 3 && zekrTime.Count < 11 ? "Times" : "Time";
                counterLBL.Content = (Settings.Default.currentLang == "ar" ? ZekrIndexCount + " " + resource[timeInFileIndex] : resource[timeInFileIndex] + " " + ZekrIndexCount);
                numberLBL.Content = ZekrIndex + " / " + zekrTime.Count;
                numberLBL.FlowDirection = FlowDirection.RightToLeft;

                #endregion
            }
        }
    }
}