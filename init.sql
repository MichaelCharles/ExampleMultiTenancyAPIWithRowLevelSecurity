-- Create Tenant table
CREATE TABLE IF NOT EXISTS "Tenant" (
    "TenantId" SERIAL PRIMARY KEY,
    "TenantUuid" UUID NOT NULL UNIQUE DEFAULT gen_random_uuid(),
    "Name" VARCHAR(255) NOT NULL
);

-- Create Employee table
CREATE TABLE IF NOT EXISTS "Employee" (
    "TenantId" INT NOT NULL,
    "EmployeeId" SERIAL PRIMARY KEY,
    "EmployeeUuid" UUID NOT NULL UNIQUE DEFAULT gen_random_uuid(),
    "Name" VARCHAR(255) NOT NULL,
    "Settings" JSONB NOT NULL DEFAULT '{}',
    FOREIGN KEY ("TenantId") REFERENCES "Tenant" ("TenantId") ON DELETE CASCADE
);

-- Create index on TenantId for Employee table
CREATE INDEX idx_employee_tenant_id ON "Employee" ("TenantId");

-- Insert sample data
INSERT INTO "Tenant" ("Name") VALUES
    ('Starpoint Solutions'),
    ('Globex International');

INSERT INTO "Employee" ("TenantId", "Name", "Settings") VALUES
    (1, 'John Doe', '{"HtmlEmails": true}'),
    (1, 'Jane Smith', '{"HtmlEmails": false}'),
    (2, 'Bob Johnson', '{"HtmlEmails": true}'),
    (2, 'Samantha Williams', '{"HtmlEmails": false}');

-- Create users
CREATE USER user1 WITH PASSWORD 'p4ssw0rd!';
CREATE USER user2 WITH PASSWORD 'p4ssw0rd!';

-- Grant necessary privileges to the users
GRANT USAGE ON SCHEMA public TO user1, user2;
GRANT SELECT, INSERT, UPDATE, DELETE ON "Tenant", "Employee" TO user1, user2;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO user1, user2;

-- Enable row level security on tables
ALTER TABLE "Tenant" ENABLE ROW LEVEL SECURITY;
ALTER TABLE "Employee" ENABLE ROW LEVEL SECURITY;

-- Create a function to get the current tenant ID
CREATE OR REPLACE FUNCTION get_tenant_id() RETURNS INT AS $$
BEGIN
    RETURN CASE 
        WHEN current_user = 'user1' THEN 1
        WHEN current_user = 'user2' THEN 2
        WHEN current_user = 'postgres' THEN 0  -- 0 means all tenants
        ELSE 0
    END;
END;
$$ LANGUAGE plpgsql;

-- Create policies for Tenant table
CREATE POLICY tenant_isolation_policy ON "Tenant"
    USING (get_tenant_id() = 0 OR "TenantId" = get_tenant_id());

-- Create policies for Employee table
CREATE POLICY employee_isolation_policy ON "Employee"
    USING (get_tenant_id() = 0 OR "TenantId" = get_tenant_id());

-- Set the search_path for the users
ALTER USER user1 SET search_path TO public;
ALTER USER user2 SET search_path TO public;

-- Allow postgres user to bypass RLS
ALTER TABLE "Tenant" FORCE ROW LEVEL SECURITY;
ALTER TABLE "Employee" FORCE ROW LEVEL SECURITY;