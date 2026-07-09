# Troubleshooting

## Common Issues

### Build Fails

**Error**: `CS1566: The type name 'xyz' does not exist in the type 'xyz'`

**Solution**:
```bash
dotnet clean
dotnet build
```

**Error**: `The reference assemblies for .NETFramework,Version=v4.8 were not found`

**Solution**:
- Ensure .NET 8 SDK is installed
- Check project file references correct version

---

### Database Connection

**Error**: `Connection timeout expired`

**Solution**:
1. Verify SQL Server is running
2. Check connection string in `appsettings.json`
3. Confirm database user permissions
4. Increase timeout: `Connection Timeout=30;`

**Error**: `Named Pipes Provider: Could not open a connection to SQL Server`

**Solution**:
- Enable TCP/IP in SQL Server Configuration Manager
- Use IP address instead of server name: `Server=127.0.0.1;`

---

### Migration Issues

**Error**: `There is no pending migration`

**Solution**:
```bash
# Create new migration first
dotnet ef migrations add MyMigration
```

**Error**: `An error occurred while accessing the Microsoft.EntityFrameworkCore`

**Solution**:
```bash
# Reinstall EF tools
dotnet tool uninstall -g dotnet-ef
dotnet tool install -g dotnet-ef
```

**Error**: `The model backing the DbContext has changed since the database was created`

**Solution**:
```bash
# Option 1: Create new migration
dotnet ef migrations add UpdateSchema
dotnet ef database update

# Option 2: Recreate database (development only)
dotnet ef database drop
dotnet ef database update
```

---

### Authentication Issues

**Error**: `Invalid token` or `Token expired`

**Solution**:
1. Verify JWT secret key in `appsettings.json`
2. Check token expiration time
3. Regenerate token by logging in again

**Error**: `401 Unauthorized` on protected endpoint

**Solution**:
1. Include JWT token in Authorization header:
   ```
   Authorization: Bearer <your-token>
   ```
2. Verify token is valid (not expired)
3. Check user role for endpoint

**Error**: `403 Forbidden` despite valid token

**Solution**:
- Endpoint requires specific role (e.g., Admin)
- Verify user has required role in database
- Check role claim in token

---

### API Errors

**Error**: `400 Bad Request` with validation errors

**Solution**:
1. Check request body format matches expected DTO
2. Verify all required fields are provided
3. Validate field types (string, number, UUID)
4. See error response for specific field issues

**Error**: `404 Not Found`

**Solution**:
- Verify resource ID exists in database
- Check ID format (UUID vs int)
- Confirm endpoint path is correct

**Error**: `409 Conflict`

**Solution**:
- Duplicate entry (e.g., email already exists)
- Unique constraint violated
- Check database for existing record

**Error**: `429 Too Many Requests`

**Solution**:
- Exceeded rate limit (200 requests/minute)
- Wait before retrying
- Implement exponential backoff in client

---

### Performance Issues

**Problem**: Slow API responses

**Solution**:
1. Check query execution time in logs
2. Enable query logging in `appsettings.Development.json`:
   ```json
   "Logging": {
     "Microsoft.EntityFrameworkCore": "Debug"
   }
   ```
3. Look for N+1 queries (missing `.Include()`)
4. Check database indexes exist
5. Profile with SQL Server Profiler

**Problem**: High memory usage

**Solution**:
1. Implement pagination for large datasets
2. Project only needed fields in queries
3. Use `.AsNoTracking()` for read-only queries
4. Clear tracking context periodically

---

### Deployment Issues

**Error**: `The type initializer for 'MyNamespace.MyClass' threw an exception`

**Solution**:
- Verify all configuration is available in deployment environment
- Check environment variables are set
- Ensure database connection string is accessible

**Error**: `The database cannot be opened because it is read-only`

**Solution**:
- Check file permissions on database files
- Verify SQL Server service has write access
- Restore from backup if corrupted

**Error**: `Web application failed to start after deployment`

**Solution**:
1. Check application logs
2. Verify all dependencies installed
3. Confirm configuration values are set
4. Test locally before redeploying

---

### Debugging Tips

### Enable Detailed Logging
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug",
      "Microsoft.EntityFrameworkCore.Database.Command": "Debug"
    }
  }
}
```

### View Query Logs
SQL queries logged in console/debug window when using `Debug` log level.

### Inspect Request/Response
Use Swagger/Postman to test endpoints with full request/response visibility.

### Database Queries
```bash
# Connect to SQL Server
sqlcmd -S localhost -U sa -P YourPassword

# View recent queries
SELECT * FROM sys.dm_exec_recent_expensive_queries;
```

---

## Getting Help

1. **Check logs** - Usually reveals root cause
2. **Search issues** - GitHub repository issues
3. **Consult documentation** - docs/ folder
4. **Stack Overflow** - Tag with relevant tech (.net, entity-framework, etc.)
