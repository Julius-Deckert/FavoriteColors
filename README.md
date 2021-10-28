# Bearbeitung des Assecor Assessment Tests (https://github.com/Assecor-GmbH/assecor-assessment-backend.git)

* Das Einlesen der .csv-Datei wurde so gehandhabt, dass einem regulären Ausdruck zufolge nur gültige persönliche Daten eingelesen werden (alle persönlichen Informationen vorhanden und im richtigen Format).
Wie gefordert wurde die .csv-Datei nicht verändert.
* Die drei geforderten GET- sowie POST-Anfragemethoden wurden implementiert. Demenstrechend wurden Unit Tests erstellt welche die möglichen Http-Antworten testen.
* Eine weiter Datenquelle in Form der MongoDb wurde zusätzlich angebunden.

## Schwachstelle der aktuellen Implementierung !

Die Vergabe der Ids wird über Integer geregelt (durch Vorgabe in der Aufgabe). Bei der POST-Operation wird aktuell deshalb noch geprüft ob diese Id bereits existiert. Diese Prüfung könnte man in Zukunft durch die Verwendung von Guids für Personen obsolet machen.

