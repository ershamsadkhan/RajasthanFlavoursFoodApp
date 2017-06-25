update offer set OfferCode='C200' ,OfferHeader='10% Discount on purchase over Rs 2000',
OfferDescription='Customer who have ordered dishes worth Rs 2000 in a month will get 10% Discount' where offerid=1

update offer set OfferCode='C400' ,OfferHeader='20% Discount on purchase over Rs 4000',
OfferDescription='Customer who have ordered dishes worth Rs 4000 in a month will get 20% Discount' where offerid=2

insert into offer values('COM100','Complementry Rice on every Purchase','When ordering Full Plate any Dish get One Full plate Rice .With Medium get Medium plate rice and with Quarter get Quarter Plate Rice',1,0,0,'2017-08-28')

insert into offer values('BD100','Birthday Special','Birthday Special ,On providing proof of your birthday (Pan , Passport , Driving License etc)you will get straight 20% Discount',20,0,0,'2017-08-28')
