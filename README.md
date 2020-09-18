# Photo Tools

## PhotoMetadata

### Metadata sources
Metadata comes from two sources
- Excel spreadsheets, each sheet (tab) represents a folder of photos and each row on a sheet is an individual photo. The tab name is the folder/film ID.
- A consolidated json file generated from the last data extract from the excel files
 
 Photo files are scanned to extract the Film ID and the Photo ID
 - The Film ID is the parent directory name for directories with image files in

 Folder/film ID's must be unique and have one of the following formats:- 
 - ```<nnn>-<nnn>``` This is for APS films and is the number printed on the cartridge by the manufacturer
 - ```<yyyy>-<mm>``` This is for digital photos and is the year and month the photo was taken
- ```<nnnnnn>``` This is a general format mostly for 35mm film but is also used as a fallback for other film formats. The preference is to get the six digit number from the film strip. This is usually the number assigned when the film was processed. If the number is less than 6 digits it has padded out with leading zeroes. Slides don't have a film processing number so the 6 digit film id has been derived by combining the year and month the slide was printed (i.e. yyyymm).
- ```<nnnnnn> <nnnnnn>``` This is a special case for slide sets that were printed on the same year and month. The first six digit number is the combined year and month and the second 6 digit number is a randomly generated unique film id. If none the the above methods are available then a random 6 digit value has been assigned for the film.
