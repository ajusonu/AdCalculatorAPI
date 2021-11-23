# AdCalculatorAPI
AdCalculatorAPI

## Calculate the price of given list of advertisements based on its Length, Type and Station of Advertisement.

<!-- Task List -->
* [X] Class Library Created
* [X] Unit Testing Done
* [X] API Created
* [ ] Refactoring Doing
* [ ] Log4Net to log Error/Exceptions
* [ ] Impliment Authentication 
* [ ] Refacrtoring into Repository-Service Pattern with SQL Advertisement Repository

## API Information

> Sample Input/Output
 
 ![RequestInputOutput](https://user-images.githubusercontent.com/46036272/142950642-ff060745-8561-455f-aca6-0954f24ca0f4.png)
---
> ### Body Parameters
#### Collection of [Advertisement](http:)
|Name           |Type                                |
|---------------|------------------------------------|
|Id             | integer                            |
|Length         | integer                            |
|Type           | [Advertisement Type]()        |
|Station        | [Radio Station]()             |

---
> #### Advertisement Type
Possible enumeration values:
|Name           |Value                         |
|---------------|------------------------------|
|Video          | 0                            |
|Radio          | 1                            |

> #### Radio Station
Possible enumeration values:
---
|Name           |Value                         |
|---------------|------------------------------|
|STAR_WARS_FM   | 0                            |
|PLAINS_MEN_FM  | 1                            |
|OTHER          | 2                            |



