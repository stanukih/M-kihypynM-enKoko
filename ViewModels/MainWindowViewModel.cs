using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MäkihypynTyylipisteet.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ObservableCollection<tuuli> tuulet { get; set; }
    public ObservableCollection<tuomori> tuomorit { get; set; }
        
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public int kpiste { get; set; }
    public int hypynPituus { get; set; }   
    public decimal hyviyspiste {get; set;}
    

    public MainWindowViewModel()
    {
        List<tuuli> tuuletList= new List<tuuli>();
        for (int i = 0; i < 5; i++)
            tuuletList.Add(new tuuli());
        tuulet = new ObservableCollection<tuuli>(tuuletList);
        List<tuomori> tuomoritList= new List<tuomori>();
        for (int i = 0; i < 5; i++)
            tuomoritList.Add(new tuomori());
        tuomorit = new ObservableCollection<tuomori>(tuomoritList);
    }
    
    public void laske()
    {
        Res = "";
        

        decimal hypynPituusPisteet = 0;

        decimal kerroin = 1;

        decimal summaTuomoritPisteet1 = summaTuomoritPisteet();
        Res += $"Tuomorien pisteet {summaTuomoritPisteet1}"+Environment.NewLine;
        decimal summaTuulihyvitysPisteet1 = summaTuulihyvitysPisteet();
        Res += $"Tuulet boonus {summaTuulihyvitysPisteet1}"+Environment.NewLine;
        hypynPituusPisteet += 60+(hypynPituus-kpiste)*hyviyspiste;
        Res += $"Pituus bonuus {hypynPituusPisteet}"+Environment.NewLine;
        Res += $"Yhteensä pistettä {summaTuomoritPisteet1+summaTuulihyvitysPisteet1+hypynPituusPisteet}"+Environment.NewLine;

        RaisePropertyChanged(nameof(Res));
    }

    public decimal summaTuulihyvitysPisteet()
    {
        decimal summaTuulihyvitys = 0;
        for (int i = 0; i < 5; i++)
        {
            summaTuulihyvitys += tuulet[i].pisteet;
        }
        
        decimal keskiSummaTuulihyvitys = summaTuulihyvitys /= 5;

        return keskiSummaTuulihyvitys * (kpiste - 36) / 20 * (decimal)hyviyspiste;
    }

    public decimal summaTuomoritPisteet()
    {
        decimal minTyylipisteet = 20;
        decimal maxTyylipisteet = 0;
        decimal summaTyylipisteet = 0;
        for (int i = 0; i < 5; i++)
        {
            summaTyylipisteet += tuomorit[i].pisteet;
            if (tuomorit[i].pisteet < minTyylipisteet) minTyylipisteet = tuomorit[i].pisteet;
            if (tuomorit[i].pisteet > maxTyylipisteet) maxTyylipisteet = tuomorit[i].pisteet;
        }

        return  summaTyylipisteet - (minTyylipisteet+maxTyylipisteet);
        
    }
    public string Res { get; set; }
}



public class tuuli
{
    public decimal pisteet {get; set; }
}

public class tuomori
{
    public decimal pisteet {get; set; }
}