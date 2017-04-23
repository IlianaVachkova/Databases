SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
CREATE SCHEMA IF NOT EXISTS `university` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Faculties`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Faculties` (
  `FacultyId` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NULL ,
  PRIMARY KEY (`FacultyId`) ,
  UNIQUE INDEX `FacultyId_UNIQUE` (`FacultyId` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Students`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Students` (
  `StudentsId` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NULL ,
  `Faculties_FacultyId` INT NOT NULL ,
  PRIMARY KEY (`StudentsId`, `Faculties_FacultyId`) ,
  UNIQUE INDEX `StudentsId_UNIQUE` (`StudentsId` ASC) ,
  INDEX `fk_Students_Faculties1_idx` (`Faculties_FacultyId` ASC) ,
  CONSTRAINT `fk_Students_Faculties1`
    FOREIGN KEY (`Faculties_FacultyId` )
    REFERENCES `mydb`.`Faculties` (`FacultyId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Departments`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Departments` (
  `DepartmentId` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NULL ,
  PRIMARY KEY (`DepartmentId`) ,
  UNIQUE INDEX `DepartmentId_UNIQUE` (`DepartmentId` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Professors`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Professors` (
  `ProfessorId` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NULL ,
  `Departments_DepartmentId` INT NOT NULL ,
  PRIMARY KEY (`ProfessorId`, `Departments_DepartmentId`) ,
  UNIQUE INDEX `ProfessorId_UNIQUE` (`ProfessorId` ASC) ,
  INDEX `fk_Professors_Departments1_idx` (`Departments_DepartmentId` ASC) ,
  CONSTRAINT `fk_Professors_Departments1`
    FOREIGN KEY (`Departments_DepartmentId` )
    REFERENCES `mydb`.`Departments` (`DepartmentId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Titles`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Titles` (
  `TitleId` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NULL ,
  PRIMARY KEY (`TitleId`) ,
  UNIQUE INDEX `TitleId_UNIQUE` (`TitleId` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Professors_has_Titles`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Professors_has_Titles` (
  `Professors_ProfessorId` INT NOT NULL ,
  `Titles_TitleId` INT NOT NULL ,
  PRIMARY KEY (`Professors_ProfessorId`, `Titles_TitleId`) ,
  INDEX `fk_Professors_has_Titles_Titles1_idx` (`Titles_TitleId` ASC) ,
  INDEX `fk_Professors_has_Titles_Professors_idx` (`Professors_ProfessorId` ASC) ,
  CONSTRAINT `fk_Professors_has_Titles_Professors`
    FOREIGN KEY (`Professors_ProfessorId` )
    REFERENCES `mydb`.`Professors` (`ProfessorId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Professors_has_Titles_Titles1`
    FOREIGN KEY (`Titles_TitleId` )
    REFERENCES `mydb`.`Titles` (`TitleId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `university` ;

-- -----------------------------------------------------
-- Table `university`.`Courses`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `university`.`Courses` (
  `CourseId` INT(11) NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NULL DEFAULT 'Fak' ,
  `Departments_DepartmentId` INT NOT NULL ,
  `Professors_ProfessorId` INT NOT NULL ,
  PRIMARY KEY (`CourseId`, `Departments_DepartmentId`, `Professors_ProfessorId`) ,
  UNIQUE INDEX `CourseId_UNIQUE` (`CourseId` ASC) ,
  INDEX `fk_Courses_Departments1_idx` (`Departments_DepartmentId` ASC) ,
  INDEX `fk_Courses_Professors1_idx` (`Professors_ProfessorId` ASC) ,
  CONSTRAINT `fk_Courses_Departments1`
    FOREIGN KEY (`Departments_DepartmentId` )
    REFERENCES `mydb`.`Departments` (`DepartmentId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Courses_Professors1`
    FOREIGN KEY (`Professors_ProfessorId` )
    REFERENCES `mydb`.`Professors` (`ProfessorId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `university`.`Courses_has_Students`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `university`.`Courses_has_Students` (
  `Courses_CourseId` INT(11) NOT NULL ,
  `Students_StudentsId` INT NOT NULL ,
  PRIMARY KEY (`Courses_CourseId`, `Students_StudentsId`) ,
  INDEX `fk_Courses_has_Students_Students1_idx` (`Students_StudentsId` ASC) ,
  INDEX `fk_Courses_has_Students_Courses_idx` (`Courses_CourseId` ASC) ,
  CONSTRAINT `fk_Courses_has_Students_Courses`
    FOREIGN KEY (`Courses_CourseId` )
    REFERENCES `university`.`Courses` (`CourseId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Courses_has_Students_Students1`
    FOREIGN KEY (`Students_StudentsId` )
    REFERENCES `mydb`.`Students` (`StudentsId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

USE `mydb` ;
USE `university` ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
