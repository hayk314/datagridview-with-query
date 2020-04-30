# DataGridView control supporting user defined queries

*This repository and this Readme file, in particular, are in development and are subject to change.*

``DataGridView`` control of Microsoft ``.NET`` framework is a powerful and ubiquitous tool for fetching tabular data from relational databases (e.g. Microsoft Access, MySQL, PostgreSQL, Oracle, etc) and displaying on the users' screen neatly. Usually, an SQL query would work behind the stage to populate the data to be fed into DataGridView. What happens usually, once the grid is filled, is that the end user is left alone with the data unless more control options are introduced to work and analyse the data.


This repository introduces a windows form control, an extension of the classical **DataGridView**, that allows the users to define and execute a wide range of logical queries defined in an easy and intuitive form. The queries look like plain English and are build on column names of the grid joined with logical operators ``AND``, ``OR`` and ``NOT``. Various matching operators (e.g. ``=``, ``>=``, ``Like``, etc.) can be applied on columns and the applicability of the operator to the given column is defined by the column's data type. The latter is being determined automatically, but the user is given a chance to adjust it if necessary.

``
When using this control, the user does not need to know anything about how SQL works. The tool provides a way to to build queries intiutively, in plain English.
``

## Things you can do

- create and execute a wide range of search queries on your datagrid in seconds. As far as the search operations defined in this control are fixed, the user can build ANY search query defined in logical terms (i.e. using the logical operators ``AND``, ``OR`` and ``NOT``). 
- save your query in a file and load it later on.
- create search queries using build-in wizard assistant.

## Sample usage

In your project you will need to add a reference to the ``DataGridView_withQuery.dll`` from this project's Release folder. Afterwards,  add a ``DataGridViw_withQuery``control named, say ``Datagrid_1``, on a form you wish. Consider filling the ``SearchFormTitle`` property of the grid which controls the caption of the search form (see the ``Test`` project of this repository for an explicit example).
To invoke the search functionality just call the following

```C#
this.Datagrid_1.SearchAdvancedStart();
```
which will open the advanced search form on your screen.

<p align="center">
  <img src ="https://github.com/hayk314/datagridview-with-query/blob/master/screenshots/searchGrid_onDGV.png" alt = "DataGrid Query Builder">
</p>
<p align="center">
The search query builder on a datagridview. Using this form the user can create and execute search queries involving the columns of the data grid that can be joined in query with various criteria.</b>
</p>

The main screen that is used to build the queries looks as follows.
<p align="center">
  <img src ="https://github.com/hayk314/datagridview-with-query/blob/master/screenshots/searchGrid_example.png" alt = "DataGrid Query Builder">
</p>
<p align="center">
The screen of query builder. The user can select columns of the grid on which the search will be performed. The search query is automatically transformed into plain English. You can apply the query build here  by clicking the Filter button. Follow the Commands button for more options.</b>
</p>

The **building blocks** of the query are the **columns** of the datagridview. The user can add new rows to the query builder's grid and for each row select a column from a combobox prefilled by the names of the columns of the datagridview on which the search is performed.
The image below shows this list of columns in an opened form. Each column's data type (String, Numeric, Date, Boolean) is determined automatically, and based on this types a list of search operators can be applied to the given column. For example if the data in a given column is numerical, then one can use ``=, <>, >=, <= , In Between`` operators to define the search condition involving the that column. 

<p align="center">
  <img src ="https://github.com/hayk314/datagridview-with-query/blob/master/screenshots/ColumnNameCombo.png" width="350" alt = "Columns of DataGridView in Query Builder">
</p>

The query can be build also using the **Wizard** accessible from the advanced search form (see the ``Wizard`` button). This Wizard is an interface that allows the user to build the query in a step by step fashion.

Once the search query is complete it can be used to filter the data in the grid. The queries can also be saved in a file for later use.
See the ``Commands`` button on the search screen for more options.

## Caveats

There are some caveats one needs to consider. As the development progresses this list will hopeful shrink or eliminate altogether.

- A proper error handling is currently missing in the ``.dll``
- The control has not been tested extensively yet.
- The control works on unbound grids, i.e. no fixed DataSource is supported, and the grid needs to be populated programmatically.


## How this works internally

TBA

## TODOs

TBA

### Author

`Hayk Aleksanyan`

### History

TBA

