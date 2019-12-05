#!perl

use File::Path;

my $UpdateFolder = "Tickets_Update";
$UpdateFolder =~ s/\/$//;

print "\n\n Cleaning up folder";
`rm -rf $UpdateFolder\*`;

my $cmd = 'cd proc_subdomain && git diff --name-only "origin/master..." | egrep -v "(.sqlproj|.cs|.dll|.pdb|.suo|.gitignore|Log.txt|.csproj|applicationhost.config)$" | egrep -v "dev_scripts" | egrep -v "obj\/Debug" | egrep -v "\/bin\/" && cd ..';
my $changedItems = `$cmd`;

print "\n\n Writing to procsublist.txt ";
open(my $fileProcSub, ">", "procsublist.txt") or die "$!";
print $fileProcSub "$changedItems";
close $fileProcSub;

my $cmd = 'cd proc_central && git diff --name-only "origin/master..." | egrep -v "(.sqlproj|.cs|.dll|.pdb|.suo|.gitignore|Log.txt|.csproj|applicationhost.config)$" | egrep -v "obj\/Debug" | egrep -v "\/bin\/" && cd ..';
my $changedItems = `$cmd`;


print "\n\n Writing to proccentral.txt ";
open(my $fileProcCentral, ">", "proccentral.txt") or die "$!";
print $fileProcCentral "$changedItems";
close $fileProcCentral;

print "\n\n Copying files for proc_subdomain ";

open(my $fileForCopy, "<", "procsublist.txt") or die "$!";
while (my $row = <$fileForCopy> ) {
	if ( $row !~ /^$/) {
		my $folder = $row;
		$folder =~ s/(.*)\///;
		my $fC = "./$UpdateFolder/proc_subdomain/". $1;
		#print $fC . "\n";
		if ( ! -e $fC ) {
			mkpath($fC);
		}
	}
}
close ($fileForCopy);
open(my $fileForCopy, "<", "procsublist.txt") or die "$!";
while (my $row = <$fileForCopy> ) {
	if ( $row !~ /^$/) {
		chomp($row);
		my $rowCopyLoc = $row;
		my $copyCmd = "cp -p './proc_subdomain/" . $row . "' './$UpdateFolder/proc_subdomain/${rowCopyLoc}' \n";
		#print $copyCmd;
		system($copyCmd);
	}
}
print "\n\n Copying files for proc_central";

open(my $fileForCopy, "<", "proccentral.txt") or die "$!";
while (my $row = <$fileForCopy> ) {
	if ( $row !~ /^$/) {
		my $folder = $row;
		$folder =~ s/(.*)\///;
		my $fC = "./$UpdateFolder/proc_central/". $1;
		#print $fC . "\n";
		if ( ! -e $fC ) {
			mkpath($fC);
		}
	}
}
close ($fileForCopy);
open(my $fileForCopy, "<", "proccentral.txt") or die "$!";
while (my $row = <$fileForCopy> ) {
	if ( $row !~ /^$/) {
		chomp($row);
		my $rowCopyLoc = $row;
		my $copyCmd = "cp -p './proc_central/" . $row . "' './$UpdateFolder/proc_central/${rowCopyLoc}' \n";
		#print $copyCmd;
		system($copyCmd);
	}
}

