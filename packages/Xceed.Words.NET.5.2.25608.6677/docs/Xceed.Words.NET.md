# Xceed Words for .NET – Create and manipulate Microsoft Word documents

[Xceed Words for .NET](https://xceed.com/products/net/words-for-net/) is a high-performance .NET library designed for advanced Word document processing and PDF generation. It allows developers to create, edit, convert, and secure Word and PDF documents without requiring Microsoft Office.

[![General banner](https://xceed.com/wp-content/downloads/nuget/xceed.words.net_banner2.png)](https://xceed.com/products/net/words-for-net/)

[![Xceed.Words.NET](https://xceed.com/wp-content/downloads/nuget/xceed.words.net_banner.png)](https://xceed.com/trial/?product_name=WDN)

Click the image above to receive a trial license. It unlocks all features of the component. See the [Get Started](#Get-Started) section for details.

Brought to you by [Xceed Software](https://xceed.com/), a leader in .NET components for over 25 years.

-----

## Main Features


### Word Document Processing
- Create, edit, and format `DOCX `documents programmatically
- Insert tables, images, hyperlinks, headers, and footers
- Apply advanced formatting, including styles, fonts, and paragraph alignment
- Merge, split, and manipulate Word documents
- Add text watermarks and background elements
- Apply password protection and encryption to documents
- Add digital signatures for document authentication to documents

### PDF Processing

- Convert `DOCX` to `PDF` with high accuracy

## Supported Document Formats

### Read & Write Formats


- **Microsoft Word**: `DOCX`

### Convert Word Files To


- **Fixed Layout**: `PDF`

## Compatibility

- .NET Framework 4.0 and later
- .NET 5 and later
- Runs on Windows, macOS, and Linux
- Works in desktop, web, and cloud environments
- Integrates with ASP.NET and Blazor applications


---

## Feature Overview


| Feature | Description |
|---------|------------|
| **Create & Modify Word Documents** | Create new Word documents from scratch or modify existing ones. |
| **Create PDF Documents** | Generate new PDF documents programmatically. |
| **Word to PDF Conversion** | Convert Word documents to PDF (see below for limitations). |
| **DOCX Format Support** | Supports Word documents saved in the .DOCX format (Word 2007 and up). |
| **Parallel Document Processing** | Modify multiple documents simultaneously for better performance. |
| **Template Application** | Apply templates with styles, headers, footers, properties, and content. |
| **Document Merging** | Join documents and transfer portions between them. |
| **Document Protection** | Secure documents with or without a password. |
| **Page Layout** | Set document margins and page size. |
| **Text Formatting** | Set line spacing, indentation, text direction, and alignment. |
| **Font Styling** | Manage fonts, sizes, colors, bold, underline, italic, and strikethrough. |
| **Page Numbering** | Add automatic page numbering. |
| **Sections Management** | Create and modify sections within a document. |

## **Word Elements Not Supported in PDF Conversion**

The following Word elements are **not currently supported** when converting to PDF:


- Charts
- Equations
- Watermarks
- Paragraphs in columns
- Objects, Excel tables
- Shapes / TextBoxes
- Paragraph direction from right to left
- Number of words per line may differ

## Get Started

[Xceed Words for .NET](https://xceed.com/products/net/words-for-net/) gives you the building blocks to create and manipulate Word documents using code.


Install via [NuGet](https://www.nuget.org/packages/Xceed.Words.NET):


``` cmd
dotnet add package Xceed.Words.NET
``````

Add the [license key](https://xceed.com/trial/?product_name=WDN) in your application code before starting to use the component’s classes:

```csharp
static void Main( string[] args )
{
  Xceed.Words.NET.Licenser.LicenseKey = "WDNXX-XXXXX-XXXXX-XXXX";

  DocX document = DocX.Create( "MyDocument.docx" );
  /* ... */
}
```

Read the full documentation licensing article [here](https://xceed.com/documentation/xceed-document-libraries-for-net/Licensing.html).

# More from Xceed

[Xceed](https://xceed.com/products/all/) offers additional high‑quality libraries for .NET developers.

## [Xceed PDF Library for .NET](https://www.nuget.org/packages/Xceed.PdfLibrary.NET#readme-body-tab)

Create, load, edit, and save PDF documents without requiring Adobe Acrobat or Microsoft Office.

## [Xceed Workbooks for .NET](https://www.nuget.org/packages/Xceed.Workbooks.NET#readme-body-tab)

Create, load, edit, and save Excel workbooks and spreadsheet files, without needing Microsoft Excel or Office interop.


---

# Documentation and Resources


| Resource           | Link |
|--------------------|------|
| **Product Page and Pricing** | [Xceed Words for .NET](https://xceed.com/products/net/words-for-net/) |
| **Documentation** | [Online Documentation](https://xceed.com/documentation/xceed-document-libraries-for-net/index.html) |
| **NuGet Package**  | [NuGet Package](https://www.nuget.org/packages/Xceed.Words.NET) |
| **GitHub Repository** | [GitHub Repo](https://github.com/xceedsoftware/DocX) |
| **Example Projects** | [Code Examples](https://github.com/xceedsoftware/DocX/tree/master/Xceed.Words.NET.Examples/Samples) |
| **Blog & Updates** | [Xceed Blog](https://xceed.com/blog/) |
| **Support Forum**  | [Xceed Support](https://xceed.com/support/) |
| **License Details** | [Xceed Licensing](https://xceed.com/license-agreement/) |

