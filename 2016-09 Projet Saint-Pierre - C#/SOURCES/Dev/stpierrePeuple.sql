-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Ven 25 Novembre 2016 à 15:55
-- Version du serveur :  10.1.19-MariaDB
-- Version de PHP :  5.6.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `stpierre`
--
CREATE DATABASE IF NOT EXISTS `stpierre` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `stpierre`;

-- --------------------------------------------------------

--
-- Structure de la table `brand`
--

DROP TABLE IF EXISTS `brand`;
CREATE TABLE IF NOT EXISTS `brand` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `phone` varchar(30) DEFAULT NULL,
  `contact` varchar(255) DEFAULT NULL,
  `website` varchar(127) DEFAULT NULL,
  `note` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `brand`
--

INSERT INTO `brand` (`id`, `name`, `phone`, `contact`, `website`, `note`) VALUES
(5, 'Castrol', '1-514-CAS-TROL', 'Castrol contact - Bernard', 'castrol.com', 'Marque d''huiles et d''articles de maintenance'),
(6, 'Master', '819-123-0012', 'Master contact - Gaétan', 'master.ca', 'Marque d''outils et d''équipement'),
(3, 'Ford', '819-221-FORD', 'Ford contact - Gérard', 'ford.ca', 'Marque de véhicules utilisés.'),
(4, 'Harvey', '819-HAR-VEY0', 'Harvey contact - Guy', 'harvey.ca', 'Restaurant préféré des employés'),
(2, 'CAT', '819-CAT-1234', 'Cat contact - Bob', 'cat.ca', 'Marque de machineries lourdes'),
(1, 'Inconnue', 'Inconnue', 'Aucun', 'www.google.com', 'La marque de le objet est inconnue');

-- --------------------------------------------------------

--
-- Structure de la table `category`
--

DROP TABLE IF EXISTS `category`;
CREATE TABLE IF NOT EXISTS `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `category`
--

INSERT INTO `category` (`id`, `name`, `description`) VALUES
(2, 'Articles', 'Accessoires pour les équipements'),
(1, 'Équipement', 'Items ayant un moteur'),
(3, 'Autre', 'Items divers, sans lien avec les équipements ou articles');

-- --------------------------------------------------------

--
-- Structure de la table `company`
--

DROP TABLE IF EXISTS `company`;
CREATE TABLE IF NOT EXISTS `company` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(127) NOT NULL,
  `description` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `company`
--

INSERT INTO `company` (`id`, `name`, `description`) VALUES
(3, 'Transport St-Pierre', ''),
(2, 'Excavation St-Pierre', ''),
(4, 'Démolition St-Pierre', ''),
(1, 'Inconnue', 'Le la compagnie pour la qu''elle l''objet est attitré est inconnue ou pour plusieurs de ceux-ci');

-- --------------------------------------------------------

--
-- Structure de la table `item`
--

DROP TABLE IF EXISTS `item`;
CREATE TABLE IF NOT EXISTS `item` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` text NOT NULL,
  `number` varchar(10) DEFAULT NULL,
  `model` varchar(127) DEFAULT NULL,
  `year` smallint(6) DEFAULT NULL,
  `value` decimal(10,0) DEFAULT NULL,
  `comments` text,
  `receptionDate` datetime DEFAULT NULL,
  `creationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `serialNumber` varchar(255) DEFAULT NULL,
  `matriculation` varchar(10) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `FK_Type_id` int(11) NOT NULL,
  `FK_Brand_id` int(11) DEFAULT NULL,
  `FK_Location_id` int(11) DEFAULT NULL,
  `FK_Provider_id` int(11) DEFAULT NULL,
  `FK_Company_id` int(11) DEFAULT NULL,
  `FK_Unit_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `item`
--

INSERT INTO `item` (`id`, `name`, `description`, `number`, `model`, `year`, `value`, `comments`, `receptionDate`, `creationDate`, `serialNumber`, `matriculation`, `quantity`, `FK_Type_id`, `FK_Brand_id`, `FK_Location_id`, `FK_Provider_id`, `FK_Company_id`, `FK_Unit_id`) VALUES
(2, 'Bouteur B8N', 'Voici un autre bouteur d8n', 'b8n2', 'b8n', 2007, '65000', 'Voici un autre bouteur d8n', '2006-05-05 13:50:00', '2016-10-13 20:38:49', 'B8N10124', '1234', NULL, 1, 1, 1, 1, 1, NULL),
(1, 'Bouteur B8N', 'Voici le bouteur d8n', 'b8n1', 'b8n', 1997, '45000', 'Voici le bouteur d8n', '1999-05-05 13:50:00', '2016-10-13 20:37:36', 'B8N10123', 'B8N1A2', NULL, 4, 2, 3, 1, 2, NULL),
(3, 'Vielle Pelle Mécanique P1M', 'Voici une pelle mécanique de modèle P1M', 'P1M1', 'P1M', 2006, '60000', 'Voici une pelle mécanique de modèle P1M', '2006-03-03 12:00:00', '2016-10-13 20:41:13', 'P1M001', 'P1M1P1', NULL, 5, 3, 1, 1, 1, NULL),
(4, 'Nouvelle Pelle Mécanique P1M', 'Voici une pelle mécanique de modèle P1M', 'P1M2', 'P1M', 2012, '90000', 'Voici une pelle mécanique de modèle P1M', '2012-03-03 12:00:00', '2016-10-13 20:41:58', 'P1M002', 'P1M2P1', NULL, 6, 4, 4, 2, 3, NULL),
(5, 'Pelle P01', 'Pelle P01 pour la pelle mécanique P1M', 'P01-1', 'P01', 1997, '4500', 'Pelle P01 pour la pelle mécanique P1M', '2002-10-19 00:00:00', '2016-10-13 20:45:04', 'P01-01', NULL, NULL, 2, 5, 1, 1, 1, NULL),
(6, 'Nouvelle Pelle P01', 'Nouvelle Pelle P01', 'P01-2', 'P01', 2015, '10500', 'TEST\nPelle P01 pour la pelle mécanique P1M', '2015-10-19 00:00:00', '2016-10-13 20:45:43', 'P01-02', NULL, NULL, 2, 6, 1, 1, 1, NULL),
(7, 'Huile biodégradable', 'Huile bio [xyz]', 'HBI-01', 'HBI', NULL, '750', 'Voici de l''huile biodégradable pour les chantiers près des cours d''eau', '2016-10-04 00:00:00', '2016-10-13 20:51:45', NULL, NULL, 200, 3, 1, 1, 3, 1, 1),
(8, 'Huile', 'Voici de l''huile pour les chantiers habituels', 'H-01', 'HUILE', NULL, '750', 'Voici de l''huile pour les chantiers habituels', '2016-07-04 00:00:00', '2016-10-13 20:52:36', NULL, NULL, 150, 3, 1, 1, 1, 1, 1),
(19, 'Vis', 'Vis caré #2', 'C2', 'Vis caré #2', NULL, NULL, 'useless', NULL, '2016-11-11 09:56:12', NULL, NULL, NULL, 1, 3, 2, 3, 4, NULL),
(20, 'Bouteur B8N', 'Voici un autre bouteur d8n', 'b8n22', 'b8nasd', 2007, '65000', 'Voici un autre bouteur d8n', '2006-05-05 13:50:00', '2016-11-25 09:42:00', 'B8N10124', '1234', NULL, 1, 1, 1, 1, 1, NULL),
(23, 'Bouteur B8N', 'Voici un autre bouteur d8n', '', 'b8nasd', 2007, '65000', 'Voici un autre bouteur d8n', '2006-05-05 13:50:00', '2016-11-25 09:46:48', 'B8N10124', '1234', NULL, 1, 1, 1, 1, 1, NULL);

-- --------------------------------------------------------

--
-- Structure de la table `item_compatibility`
--

DROP TABLE IF EXISTS `item_compatibility`;
CREATE TABLE IF NOT EXISTS `item_compatibility` (
  `FK_Item1_id` int(11) NOT NULL,
  `FK_Item2_id` int(11) NOT NULL,
  PRIMARY KEY (`FK_Item1_id`,`FK_Item2_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `item_compatibility`
--

INSERT INTO `item_compatibility` (`FK_Item1_id`, `FK_Item2_id`) VALUES
(1, 8),
(1, 9),
(2, 7),
(2, 9),
(3, 8),
(3, 9),
(4, 8),
(4, 9),
(5, 8),
(5, 9),
(6, 7),
(6, 9);

-- --------------------------------------------------------

--
-- Structure de la table `location`
--

DROP TABLE IF EXISTS `location`;
CREATE TABLE IF NOT EXISTS `location` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(127) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `location`
--

INSERT INTO `location` (`id`, `name`, `description`) VALUES
(2, 'Hangar Sherbrooke', 'Hangar principale de la maison mère'),
(3, 'Entrepôt secondaire sherbrooke', 'Entrepôt secondaire près du pont'),
(4, 'Chantier 1 Québec', 'La pelle est au chantier de pont ***'),
(1, 'Inconnue', 'L''endroit est inconnue');

-- --------------------------------------------------------

--
-- Structure de la table `provider`
--

DROP TABLE IF EXISTS `provider`;
CREATE TABLE IF NOT EXISTS `provider` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `phone` varchar(30) DEFAULT NULL,
  `phoneAlt` varchar(30) DEFAULT NULL,
  `email` varchar(127) DEFAULT NULL,
  `contact` varchar(255) DEFAULT NULL,
  `website` varchar(127) DEFAULT NULL,
  `city` varchar(127) DEFAULT NULL,
  `notes` text,
  `creationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `provider`
--

INSERT INTO `provider` (`id`, `name`, `phone`, `phoneAlt`, `email`, `contact`, `website`, `city`, `notes`, `creationDate`) VALUES
(2, 'First provider', '123 456 7890', 'tmp', 'test@email.com', 'bobby bob', 'bob.ca', 'city', 'Notes', '2016-10-29 14:57:32'),
(3, 'Second provider', '223 456 7890', 'tmp', 'test2@email.com', '2bobby bob', 'bob.2ca', 'city2', 'Notes2', '2016-10-29 14:57:53'),
(1, 'Inconnue', 'Aucun', 'Aucun', 'Aucun', 'Aucun', 'Aucun', 'Aucun', 'Aucun', '2016-11-11 10:07:01');

-- --------------------------------------------------------

--
-- Structure de la table `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `role`
--

INSERT INTO `role` (`id`, `name`, `description`) VALUES
(1, 'Administration', 'L''administration peut voir tout les champs ainsi que de créer et modifier les autres utilisateurs'),
(2, 'Mécanicien', 'Le mécanicien est l''utilisateur général.\r\nIl ne peut modifier les informations, Il peut seulement lire certains champs. ');

-- --------------------------------------------------------

--
-- Structure de la table `type`
--

DROP TABLE IF EXISTS `type`;
CREATE TABLE IF NOT EXISTS `type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `FK_Category_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `type`
--

INSERT INTO `type` (`id`, `name`, `FK_Category_id`) VALUES
(5, 'Pelle', 2),
(4, 'Pick-Up', 1),
(3, 'Pelle Mécanique', 1),
(2, 'Bouteur', 1),
(6, 'Cisailles', 2),
(7, 'Outils', 3),
(8, 'Maintenance', 3),
(1, 'Inconnue', 3);

-- --------------------------------------------------------

--
-- Structure de la table `unit`
--

DROP TABLE IF EXISTS `unit`;
CREATE TABLE IF NOT EXISTS `unit` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `shortName` varchar(10) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `unit`
--

INSERT INTO `unit` (`id`, `name`, `shortName`, `description`) VALUES
(2, 'Litres', 'L', 'Volume en Litres'),
(1, 'Kilogramme', 'kg', 'Poids en Kg'),
(3, 'onces', 'oz', 'Quantité liquide onces'),
(4, 'livre', 'lbs', 'Poids en livre');

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `firstName` varchar(127) NOT NULL,
  `lastName` varchar(127) NOT NULL,
  `email` varchar(127) NOT NULL,
  `phone` varchar(30) NOT NULL,
  `phoneAlt` varchar(30) DEFAULT NULL,
  `userCode` char(6) NOT NULL,
  `passwordHash` varchar(255) NOT NULL,
  `note` text,
  `creationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `archiveDate` datetime DEFAULT NULL,
  `isActive` tinyint(1) NOT NULL DEFAULT '1',
  `FK_Role_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `user`
--

INSERT INTO `user` (`id`, `username`, `firstName`, `lastName`, `email`, `phone`, `phoneAlt`, `userCode`, `passwordHash`, `note`, `creationDate`, `archiveDate`, `isActive`, `FK_Role_id`) VALUES
(1, 'first', 'firstname1', 'lastname1', 'first@user.ca', '1234567890', NULL, 'user', '12dea96fec20593566ab75692c9949596833adc9', 'notes', '2016-10-29 15:00:36', NULL, 1, 2),
(2, 'test', 'test', 'test', 'test@gmail.com', '819-444-1919', '819-444-1919', 'test', '', 'Utilisateur test', '2016-11-11 10:10:48', '2016-11-30 00:00:00', 1, 2),
(3, 'Admin', 'admin', 'admin', 'admin@gmail.com', '911', '911', 'admin', 'dd94709528bb1c83d08f3088d4043f4742891f4f', 'Administrateur du système', '2016-11-11 10:11:44', NULL, 1, 1);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
