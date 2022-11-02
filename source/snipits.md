
find dependent upon items in csharp

```
>$\n\s*<DependentUpon>[^\n]*\n\s*</Compile>
```

For example to find elements dependent on `wave.cs`
to replace with ` />`.

```
>$\n\s*<DependentUpon>wave.cs[^\n]*\n\s*</Compile>
```