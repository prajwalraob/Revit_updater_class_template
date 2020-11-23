# Revit Updater Class Template

This repository contains a template for using the IUpdater interface provided by Revit API

The IUpdater Interface can be used when you want revit to automatically trigger a process when changes are made to the project file.

The Autodesk.Revit.ApplicationServices.ControlledApplication.DocumentChanged Event can also be used to perform a similar task but IUpdater Interface offers more flexibility for this

Refer the documentation for [IUpdater](https://www.revitapidocs.com/2015/4cdaf502-fc25-8f18-7618-8448cce33d11.htm) Interface and [DocumentChanged](https://www.revitapidocs.com/2015/f7acc5b4-a1b4-12ca-802b-0ee78942589e.htm) Event for more details.
