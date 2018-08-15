using System;

namespace Project.Entity
{
	 public class Entity_T_ResourcePrice
	 {
		 private Int32 _iD;
		 private string _resourceID;
		 private Boolean _yearEnable;
		 private Decimal _yearInPrice;
		 private Decimal _yearOutPrice;
		 private Boolean _quaterEnable;
		 private Decimal _quaterInPrcie;
		 private Decimal _quaterOutPrice;
		 private Boolean _monthEnable;
		 private Decimal _monthInPrice;
		 private Decimal _monthOutPrice;
		 private Boolean _weekEnable;
		 private Decimal _weekInPrice;
		 private Decimal _weekOutPrice;
		 private Boolean _dayEnable;
		 private Decimal _dayInPrice;
		 private Decimal _dayOutPrice;
		 private Boolean _hDayEnable;
		 private Decimal _hDayInPrice;
		 private Decimal _hDayOutPrice;
		 private Boolean _hourEnable;
		 private Decimal _hourInPrice;
		 private Decimal _hourOutPrice;
		 private Boolean _singleEnable;
		 private Decimal _singleInPrice;
		 private Decimal _singleOutPrice;
		 private Boolean _delayEnable;
		 private Decimal _delayInPrice;
		 private Decimal _delayOutPrice;
		 private Boolean _meterEnable;
		 private Decimal _meterMinPrice;
		 private Decimal _meterMaxPrice;
		 private Boolean _iMonthEnable;
		 private Decimal _iMonthMinPrice;
		 private Decimal _iMonthMaxPrice;
		 private Boolean _iSingleEnable;
		 private Decimal _iSingleMinPrice;
		 private Decimal _iSingleMaxPrice;
		 private Boolean _onceEnable;
		 private Decimal _onceMinPrice;
		 private Decimal _onceMaxPrice;
		 private Boolean _otherEnable;
		 private Decimal _otherMinPrice;
		 private Decimal _otherMaxPrice;

		 /// <summary>
		 /// 缺省构造函数
		 /// </summary>
		 public Entity_T_ResourcePrice(){}

		 public Int32 ID
		 {
			 get { return _iD; }
			 set { _iD = value; }
		 }
		 public string ResourceID
		 {
			 get { return _resourceID; }
			 set { _resourceID = value; }
		 }
		 public Boolean YearEnable
		 {
			 get { return _yearEnable; }
			 set { _yearEnable = value; }
		 }
		 public Decimal YearInPrice
		 {
			 get { return _yearInPrice; }
			 set { _yearInPrice = value; }
		 }
		 public Decimal YearOutPrice
		 {
			 get { return _yearOutPrice; }
			 set { _yearOutPrice = value; }
		 }
		 public Boolean QuaterEnable
		 {
			 get { return _quaterEnable; }
			 set { _quaterEnable = value; }
		 }
		 public Decimal QuaterInPrcie
		 {
			 get { return _quaterInPrcie; }
			 set { _quaterInPrcie = value; }
		 }
		 public Decimal QuaterOutPrice
		 {
			 get { return _quaterOutPrice; }
			 set { _quaterOutPrice = value; }
		 }
		 public Boolean MonthEnable
		 {
			 get { return _monthEnable; }
			 set { _monthEnable = value; }
		 }
		 public Decimal MonthInPrice
		 {
			 get { return _monthInPrice; }
			 set { _monthInPrice = value; }
		 }
		 public Decimal MonthOutPrice
		 {
			 get { return _monthOutPrice; }
			 set { _monthOutPrice = value; }
		 }
		 public Boolean WeekEnable
		 {
			 get { return _weekEnable; }
			 set { _weekEnable = value; }
		 }
		 public Decimal WeekInPrice
		 {
			 get { return _weekInPrice; }
			 set { _weekInPrice = value; }
		 }
		 public Decimal WeekOutPrice
		 {
			 get { return _weekOutPrice; }
			 set { _weekOutPrice = value; }
		 }
		 public Boolean DayEnable
		 {
			 get { return _dayEnable; }
			 set { _dayEnable = value; }
		 }
		 public Decimal DayInPrice
		 {
			 get { return _dayInPrice; }
			 set { _dayInPrice = value; }
		 }
		 public Decimal DayOutPrice
		 {
			 get { return _dayOutPrice; }
			 set { _dayOutPrice = value; }
		 }
		 public Boolean HDayEnable
		 {
			 get { return _hDayEnable; }
			 set { _hDayEnable = value; }
		 }
		 public Decimal HDayInPrice
		 {
			 get { return _hDayInPrice; }
			 set { _hDayInPrice = value; }
		 }
		 public Decimal HDayOutPrice
		 {
			 get { return _hDayOutPrice; }
			 set { _hDayOutPrice = value; }
		 }
		 public Boolean HourEnable
		 {
			 get { return _hourEnable; }
			 set { _hourEnable = value; }
		 }
		 public Decimal HourInPrice
		 {
			 get { return _hourInPrice; }
			 set { _hourInPrice = value; }
		 }
		 public Decimal HourOutPrice
		 {
			 get { return _hourOutPrice; }
			 set { _hourOutPrice = value; }
		 }
		 public Boolean SingleEnable
		 {
			 get { return _singleEnable; }
			 set { _singleEnable = value; }
		 }
		 public Decimal SingleInPrice
		 {
			 get { return _singleInPrice; }
			 set { _singleInPrice = value; }
		 }
		 public Decimal SingleOutPrice
		 {
			 get { return _singleOutPrice; }
			 set { _singleOutPrice = value; }
		 }
		 public Boolean DelayEnable
		 {
			 get { return _delayEnable; }
			 set { _delayEnable = value; }
		 }
		 public Decimal DelayInPrice
		 {
			 get { return _delayInPrice; }
			 set { _delayInPrice = value; }
		 }
		 public Decimal DelayOutPrice
		 {
			 get { return _delayOutPrice; }
			 set { _delayOutPrice = value; }
		 }
		 public Boolean MeterEnable
		 {
			 get { return _meterEnable; }
			 set { _meterEnable = value; }
		 }
		 public Decimal MeterMinPrice
		 {
			 get { return _meterMinPrice; }
			 set { _meterMinPrice = value; }
		 }
		 public Decimal MeterMaxPrice
		 {
			 get { return _meterMaxPrice; }
			 set { _meterMaxPrice = value; }
		 }
		 public Boolean IMonthEnable
		 {
			 get { return _iMonthEnable; }
			 set { _iMonthEnable = value; }
		 }
		 public Decimal IMonthMinPrice
		 {
			 get { return _iMonthMinPrice; }
			 set { _iMonthMinPrice = value; }
		 }
		 public Decimal IMonthMaxPrice
		 {
			 get { return _iMonthMaxPrice; }
			 set { _iMonthMaxPrice = value; }
		 }
		 public Boolean ISingleEnable
		 {
			 get { return _iSingleEnable; }
			 set { _iSingleEnable = value; }
		 }
		 public Decimal ISingleMinPrice
		 {
			 get { return _iSingleMinPrice; }
			 set { _iSingleMinPrice = value; }
		 }
		 public Decimal ISingleMaxPrice
		 {
			 get { return _iSingleMaxPrice; }
			 set { _iSingleMaxPrice = value; }
		 }
		 public Boolean OnceEnable
		 {
			 get { return _onceEnable; }
			 set { _onceEnable = value; }
		 }
		 public Decimal OnceMinPrice
		 {
			 get { return _onceMinPrice; }
			 set { _onceMinPrice = value; }
		 }
		 public Decimal OnceMaxPrice
		 {
			 get { return _onceMaxPrice; }
			 set { _onceMaxPrice = value; }
		 }
		 public Boolean OtherEnable
		 {
			 get { return _otherEnable; }
			 set { _otherEnable = value; }
		 }
		 public Decimal OtherMinPrice
		 {
			 get { return _otherMinPrice; }
			 set { _otherMinPrice = value; }
		 }
		 public Decimal OtherMaxPrice
		 {
			 get { return _otherMaxPrice; }
			 set { _otherMaxPrice = value; }
		 }
	 }
}
