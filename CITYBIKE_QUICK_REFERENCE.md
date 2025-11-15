# QUICK REFERENCE - MVC OPTIMIZATION PATTERNS

## 🎯 PARTIAL VIEWS (Use for Static Reusable HTML)

### When to Use:
- Same HTML structure repeated multiple times
- No complex logic needed
- Simple parameter passing

### Syntax:
```razor
@await Html.PartialAsync("_PartialName", model)
@await Html.PartialAsync("_PartialName", (param1, param2))
```

### Examples Created:
```razor
@* Hero Section *@
@await Html.PartialAsync("_CitybikeHero", ("TITLE", "Subtitle"))

@* Form Buttons *@
@await Html.PartialAsync("_CitybikeFormActions", ("Submit", "Index"))

@* Pagination *@
@await Html.PartialAsync("_CitybikePagination", Model)

@* Delete Warning *@
@await Html.PartialAsync("_CitybikeDeleteWarning", ("TITLE", "entity"))
```

---

## 📋 DISPLAY TEMPLATES (Use for Auto-Rendering Models)

### When to Use:
- Rendering collections of items
- Automatic formatting of model properties
- Consistent display across application

### Location: `Views/Shared/DisplayTemplates/ModelName.cshtml`

### Syntax:
```razor
@* Automatic (by type) *@
@Html.DisplayFor(m => item)

@* Explicit template *@
@Html.DisplayFor(m => item, "TemplateName")
```

### Example:
**DisplayTemplates/BiketripsMay2021.cshtml:**
```razor
@model BiketripsMay2021
<div class="citybike-trip-card">
    <!-- Card HTML -->
</div>
```

**Usage in Index.cshtml:**
```razor
@foreach (var item in Model)
{
    @Html.DisplayFor(m => item)  @* Automatically uses template *@
}
```

---

## ✏️ EDITOR TEMPLATES (Use for Shared Forms)

### When to Use:
- Same form fields in Create and Edit
- Consistent input formatting
- Reduce form duplication

### Location: `Views/Shared/EditorTemplates/ModelName.cshtml`

### Syntax:
```razor
@Html.EditorFor(m => m)
@Html.EditorFor(m => m.Property, "TemplateName")
```

### Example:
**EditorTemplates/BiketripsMay2021.cshtml:**
```razor
@model BiketripsMay2021
<div class="citybike-form-grid">
    <div class="citybike-form-column">
        <div class="citybike-form-group">
            <label asp-for="Departure" class="citybike-label">Lähtöaika</label>
            <input asp-for="Departure" class="citybike-input" type="datetime-local" />
            <span asp-validation-for="Departure" class="citybike-validation"></span>
        </div>
        <!-- More fields -->
    </div>
</div>
```

**Usage in Create.cshtml AND Edit.cshtml:**
```razor
<form asp-action="Create">
    @Html.EditorFor(m => m)  @* Renders entire form *@
    @await Html.PartialAsync("_CitybikeFormActions", ("Save", "Index"))
</form>
```

---

## 🔧 VIEW COMPONENTS (Use for Complex Reusable Logic)

### When to Use:
- Need server-side logic in component
- Database queries in component
- Complex calculations or processing

### Syntax:
```razor
@await Component.InvokeAsync("ComponentName", parameters)
```

### Example:
**ViewComponents/TripStatsViewComponent.cs:**
```csharp
public class TripStatsViewComponent : ViewComponent
{
    private readonly DbContext _context;
    
    public async Task<IViewComponentResult> InvokeAsync(int tripId)
    {
        var stats = await CalculateStatsAsync(tripId);
        return View(stats);
    }
}
```

**Usage:**
```razor
@await Component.InvokeAsync("TripStats", new { tripId = Model.ID })
```

---

## 🏷️ TAG HELPERS (Use for Custom HTML Elements)

### When to Use:
- Create custom HTML tags
- Simplify complex HTML generation
- Better IntelliSense support

### Example:
**TagHelpers/CitybikeButtonTagHelper.cs:**
```csharp
[HtmlTargetElement("citybike-button")]
public class CitybikeButtonTagHelper : TagHelper
{
    public string Type { get; set; } = "primary";
    public string Text { get; set; }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", $"citybike-btn citybike-btn-{Type}");
        output.Content.SetHtmlContent($"<span>{Text}</span>");
    }
}
```

**Usage:**
```razor
<citybike-button type="primary" text="Tallenna" />
```

---

## 📊 COMPARISON CHART

| Pattern | Complexity | Reusability | Best For |
|---------|-----------|-------------|----------|
| **Partial Views** | Low | High | Static HTML blocks |
| **Display Templates** | Low | High | Model rendering |
| **Editor Templates** | Medium | High | Form fields |
| **View Components** | High | Medium | Components with logic |
| **Tag Helpers** | High | High | Custom HTML elements |

---

## 🎯 DECISION FLOWCHART

```
Need to reuse code?
    |
    ├─ Simple HTML without logic?
    |  └─> Use PARTIAL VIEW
    |
    ├─ Rendering model data?
    |  └─> Use DISPLAY TEMPLATE
    |
    ├─ Form input fields?
    |  └─> Use EDITOR TEMPLATE
    |
    ├─ Need server-side logic?
    |  └─> Use VIEW COMPONENT
    |
    └─ Creating custom HTML element?
       └─> Use TAG HELPER
```

---

## 🚀 QUICK CONVERSION EXAMPLES

### Before: Repeated Code
```razor
<div class="citybike-hero">
    <h1 class="citybike-title">TITLE</h1>
    <p class="citybike-subtitle">Subtitle</p>
</div>
```

### After: Partial View
```razor
@await Html.PartialAsync("_CitybikeHero", ("TITLE", "Subtitle"))
```

---

### Before: Loop with HTML
```razor
@foreach (var item in Model)
{
    <div class="card">
        <h3>@item.Name</h3>
        <p>@item.Description</p>
    </div>
}
```

### After: Display Template
```razor
@foreach (var item in Model)
{
    @Html.DisplayFor(m => item)
}
```

---

### Before: Duplicate Create/Edit Forms
**Create.cshtml (80 lines)**
**Edit.cshtml (80 lines)**

### After: Editor Template
**Create.cshtml (5 lines):**
```razor
<form asp-action="Create">
    @Html.EditorFor(m => m)
</form>
```

**Edit.cshtml (5 lines):**
```razor
<form asp-action="Edit">
    @Html.EditorFor(m => m)
</form>
```

---

## 💡 PRO TIPS

1. **Organize Partials** in subfolders:
   ```
   Views/Shared/
     Partials/
       _CitybikeHero.cshtml
       _CitybikeFormActions.cshtml
   ```

2. **Name Conventions:**
   - Partials: `_PartialName.cshtml` (underscore prefix)
   - Display Templates: `ModelName.cshtml` (exact model name)
   - Editor Templates: `ModelName.cshtml` (exact model name)

3. **Performance:**
   - Partials: Fast ✅
   - Templates: Fast ✅
   - View Components: Slightly slower (has logic) ⚠️

4. **Testing:**
   - Partials: Test the main view
   - Templates: Test by type
   - View Components: Unit testable ✅

---

## 📚 FILES CREATED IN YOUR PROJECT

✅ `Views/Shared/_CitybikeHero.cshtml`
✅ `Views/Shared/_CitybikeFormActions.cshtml`
✅ `Views/Shared/_CitybikePagination.cshtml`
✅ `Views/Shared/_CitybikeDeleteWarning.cshtml`
✅ `OPTIMIZATION_GUIDE.md`
✅ `OPTIMIZATION_SUMMARY.md`
✅ `QUICK_REFERENCE.md` (this file)

---

## 🎓 NEXT LEARNING STEPS

1. **Read:** ASP.NET Core MVC documentation on Partial Views
2. **Practice:** Convert all Station views to use partials
3. **Learn:** Create your first Display Template
4. **Master:** Build a View Component with database access
5. **Advanced:** Create custom Tag Helpers

---

**Remember:** Start simple (Partials) → Progress to advanced (Tag Helpers) as needed!
