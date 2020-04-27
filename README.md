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


## Example

<p align="center">
  <img src ="https://github.com/hayk314/datagridview-with-query/blob/master/screenshots/searchGrid_example.png" alt = "DataGrid Query Builder">
</p>
<p align="center">
The screen of query builder. The user can select columns of the grid on which the search will be performed. The search query is automatically transformed into plain English. You can apply the query build here  by clicking the Filter button. Follow the Commands button for more options.</b>
</p>


## Sample usage

In your project you will need to add a reference to the ``DataGridView_withQuery.dll`` from this project's Release folder. Afterwards,  add a ``DataGridViw_withQuery``control named, say ``Datagrid_1``, on a form you wish. To invoke the search functionality just call the following

```C#
this.Datagrid_1.SearchAdvancedStart();
```

Consider filling the ``SearchFormTitle`` property of the grid which controls the caption of the search form. 
For an explicit example, see the ``Test`` project of this repository.

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

